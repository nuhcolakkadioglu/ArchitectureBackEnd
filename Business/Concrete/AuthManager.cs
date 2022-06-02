using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Entities.Dtos.AuthDto;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthManager> _logger;
        public AuthManager(IUserService userService, ILogger<AuthManager> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public string Login(LoginAuthDto model)
        {


            var user = _userService.GetByEmail(model.Email);
            var result = HashingHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt);
            if (result)
                return "Giriş başarılı";

            return "Giriş başarısız oldu";

        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Register(RegisterAuthDto model, int imageSize)
        {
            imageSize = 1;

            //UserValidator userValidator = new UserValidator();
            //ValidationTool.Validate(userValidator, model);

            IResult result = BusinessRules.Run(CheckIfEmailExist(model.Email), CheckIfImageSize(imageSize));

            if (!result.Success)
                return result;


            _userService.Add(model);
            _logger.LogInformation(result.Message);
            return new SuccessResult(result.Message);

        }

        private IResult CheckIfEmailExist(string email)
        {
            var list = _userService.GetByEmail(email);
            if (list != null)
                return new ErrorResult("Bu mail adresi daha önce kullanılmış");

            return new SuccessResult();
        }

        private IResult CheckIfImageSize(int imageSize)
        {
            if (imageSize > 1)
                return new ErrorResult("Dosya boyutu 1mb den büyük olamaz");


            return new SuccessResult();
        }
    }
}

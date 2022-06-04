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
using Microsoft.AspNetCore.Http;
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
        public IResult Register(RegisterAuthDto model)
        {
            IResult result = BusinessRules.Run(CheckIfEmailExist(model.Email), ChechIfImageExtensionAllow(model.Image.FileName), CheckIfImageSize(model.Image.Length));

            if (result!=null)
                return result;

            _userService.Add(model);
            _logger.LogInformation("register success");
            return new SuccessResult();

        }

        private IResult CheckIfEmailExist(string email)
        {
            var list = _userService.GetByEmail(email);
            if (list != null)
                return new ErrorResult("Bu mail adresi daha önce kullanılmış");

            return new SuccessResult();
        }

        private IResult CheckIfImageSize(long imageSize)
        {
            decimal imgmbSize =Convert.ToDecimal( imageSize * 0.000001);
            
            if (imgmbSize > 1)
                return new ErrorResult("Dosya boyutu 1mb den büyük olamaz");


            return new SuccessResult();
        }
        private IResult ChechIfImageExtensionAllow(string file)
        {

            var ext = file.Substring(file.LastIndexOf('.'));
            var extension = ext.ToLower();
            List<string> AllowFileExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png" };

            if (!AllowFileExtensions.Contains(extension))
            {
                return new ErrorResult("Resim formatı desteklenmiyor.");
            }
            return new SuccessResult("Resim formatı destekleniyor.");
        }

    }
}

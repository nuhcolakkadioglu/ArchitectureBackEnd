using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
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

        public IResult Register(RegisterAuthDto model, int imageSize)
        {
            imageSize = 2;
            
            UserValidator userValidator = new UserValidator();
            ValidationTool.Validate(userValidator, model);

            if (!CheckIfEmailExist(model.Email))
            {
                _logger.LogInformation($"{model.Email} daha önceden kayıt edilmiş");
                return new ErrorResult($"{model.Email} daha önceden kayıt edilmiş");
            }
            else
            {
                if (CheckIfImageSize(imageSize))
                {
                    _userService.Add(model);
                    _logger.LogInformation("Kayıt işlemi başarılı");
                    return new SuccessResult("Kayıt işlemi başarılı");
                }
                return new ErrorResult($"Dosya boyutu 1mb den büyük olamaz");
            }
        }

        private bool CheckIfEmailExist(string email)
        {
            var list = _userService.GetByEmail(email);
            if (list != null)
                return false;

            return true;
        }
        
        private bool CheckIfImageSize(int imageSize)
        {
            if (imageSize > 1)
                return false;
                    
            return true;
        }
    }
}

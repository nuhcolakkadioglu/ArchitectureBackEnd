using Business.Abstract;
using Core.Utilities.Hashing;
using Entities.Dtos.AuthDto;
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

        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        public string Login(LoginAuthDto model)
        {
            var user = _userService.GetByEmail(model.Email);
            var result = HashingHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt);
            if(result)
                return "Giriş başarılı";

            return "Giriş başarısız oldu";
                
        }

        public void Register(RegisterAuthDto model)
        {
            _userService.Add(model);
        }
    }
}

using Business.Abstract;
using Core.Utilities.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.AuthDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(RegisterAuthDto model)
        {
           
  
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(model.Password,out passwordHash,out passwordSalt);
            User user = new()
            {
                Email = model.Email,
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt

            };
            _userDal.Add(user);
        }

        public List<User> GetAll()
        {
            return _userDal.GetAll();
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(m => m.Email == email);
        }
    }
}

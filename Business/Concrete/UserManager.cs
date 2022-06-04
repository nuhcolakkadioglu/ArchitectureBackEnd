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
        private readonly IFileService _fileService;
        public UserManager(IUserDal userDal, IFileService fileService)
        {
            _userDal = userDal;
            _fileService = fileService;
        }

        public async void Add(RegisterAuthDto model)
        {

            string fileName = _fileService.FileSave(model.Image, "./Content/img/");

           // byte[] fileByte = _fileService.FileConvertByteArrayToDatabase(model.Image);


            User user = Create(model, fileName);
            _userDal.Add(user);
        }

        private User Create(RegisterAuthDto model, string fileName)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(model.Password, out passwordHash, out passwordSalt);
            User user = new()
            {
                Email = model.Email,
                Name = model.Name,
                ImageUrl = fileName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt

            };
            return user;
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

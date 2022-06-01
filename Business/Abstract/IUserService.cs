using Entities.Concrete;
using Entities.Dtos.AuthDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        void Add(RegisterAuthDto model);
        List<User> GetAll();
        User GetByEmail(string email);
    }
}

using Entities.Dtos.AuthDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator:AbstractValidator<RegisterAuthDto>
    {
        public UserValidator()
        {
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Email).NotEmpty();
            RuleFor(m => m.Image).NotEmpty();
            RuleFor(m => m.Password).NotEmpty().MinimumLength(6);   
        }
    }
}

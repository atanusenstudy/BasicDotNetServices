using BasicDotNetServices.Core.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDotNetServices.Core.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull();
            RuleFor(x => x.LastName).NotEmpty().NotNull();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Phone).GreaterThan(1000000000);
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDotNetServices.Core.Model;
using FluentValidation;

namespace BasicDotNetServices.Core.Validator
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Phone).NotNull().GreaterThan(1000000000);
            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address).MinimumLength(5).WithMessage("Must gibe a vaid address");
            });
        }
    }
    public class ContactsValidator : AbstractValidator<List<Contact>>
    {
        public ContactsValidator()
        {
            RuleForEach(x => x).SetValidator(new ContactValidator());
        }
    }
}

using BasicDotNetServices.Core.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDotNetServices.Core.Validator
{
    internal class InstitutionValidator : AbstractValidator<Institution>
    {
        public InstitutionValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required");
            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address).MinimumLength(5).WithMessage("Must gibe a vaid address");
            });
        }
    }
}

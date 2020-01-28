using FluentValidation;
using LoanApplication.Core.Models;

namespace LoanApplication.Web.Validators
{
    public class LoanApplicantValidator : AbstractValidator<LoanApplicant>
    {
        public LoanApplicantValidator()
        {
            RuleFor(_ => _.FirstName).NotEmpty().MinimumLength(2).MaximumLength(20);
            RuleFor(_ => _.Salary).NotEmpty().LessThan(decimal.MaxValue-1);
        }
    }
}
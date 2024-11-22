using FluentValidation;
namespace ContractMonthlyClaimSystem.Models
{
    public class ClaimModelValidator : AbstractValidator<ClaimModel>
    {
        public ClaimModelValidator()
        {
            RuleFor(claim => claim.HOURLY_RATE)
                .NotEmpty().WithMessage("{PropertyName} Hourly rate is required.")
                .Must(CheckValidDouble).WithMessage("{PropertyName} Hourly rate must be a valid number.")
                .Must(CheckPositiveNumber).WithMessage("{PropertyName}  Hourly rate cannot be a negative number.");

            RuleFor(claim => claim.HOURS_WORKED)
                .NotEmpty().WithMessage("{PropertyName}  Hours worked is required.")
                .Must(CheckValidDouble).WithMessage("{PropertyName} Hours worked must ne a valid number.")
                .Must(CheckPositiveNumber).WithMessage("{PropertyName}  Hours worked cannot be a negative number");
        }
        private bool CheckValidDouble(string value)
        {
            return double.TryParse(value, out _);
        }
        private bool CheckPositiveNumber(string value)
        {
            if (double.TryParse(value, out var result))
            {
                if (result < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
}

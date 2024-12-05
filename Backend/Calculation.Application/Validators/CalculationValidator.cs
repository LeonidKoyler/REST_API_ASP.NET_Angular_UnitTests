using Calculation.Domain.Model;
using FluentValidation;


namespace Calculation.Application.Validators
{
    public class CalculationValidator : AbstractValidator<Vehicle>
    {
        public CalculationValidator()
        {
            RuleFor(x => x.BasePrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Type).NotNull();
        }
    }
}

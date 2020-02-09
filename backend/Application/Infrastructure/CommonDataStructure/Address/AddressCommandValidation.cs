using FluentValidation;

namespace Application.Infrastructure.CommonDataStructure.Address
{
    public class AddressCommandValidation : AbstractValidator<AddressCommand>
    {
        public AddressCommandValidation()
        {
                        // Validation for Country required
            RuleFor(prop => prop.CountryId)
                .LessThanOrEqualTo(246)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Country is required");

            // Validation for State required
            RuleFor(prop => prop.StateId)
                .LessThanOrEqualTo(4120)
                .GreaterThanOrEqualTo(1)
                .WithMessage("State is required");

            // Validation for Street length = 10 is avaliable
            RuleFor(prop => prop.Street)
                .MaximumLength(1000)
                .When(prop => !string.IsNullOrEmpty(prop.Street))
                .WithMessage("Not more than 1000 characters");

            // Validation for Post code length = 10 is avaliable
            RuleFor(prop => prop.PostCode)
                .MaximumLength(100)
                .When(prop => !string.IsNullOrEmpty(prop.PostCode))
                .WithMessage("Not more than 100 characters");
        }
    }
}
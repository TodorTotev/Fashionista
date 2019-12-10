namespace Fashionista.Application.Orders.Commands.Create
{
    using FluentValidation;

    using static Validation.Constants;

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            this.RuleFor(x => x.DeliveryAddressId)
                .NotEmpty()
                .WithMessage(AddressRequiredMsg);

            this.RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage(x => x.PhoneNumber);
        }
    }
}

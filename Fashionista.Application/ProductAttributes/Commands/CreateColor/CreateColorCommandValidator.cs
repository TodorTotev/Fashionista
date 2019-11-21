namespace Fashionista.Application.ProductAttributes.Commands.CreateColor
{
    using Fashionista.Domain.Entities;
    using FluentValidation;

    using static Validation.Constants;

    public class CreateColorCommandValidator : AbstractValidator<ProductColor>
    {
        public CreateColorCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .Length(NameMinLength, NameMaxLength)
                .WithMessage(string.Format(NameLengthMsg, NameMinLength, NameMaxLength));
        }
    }
}

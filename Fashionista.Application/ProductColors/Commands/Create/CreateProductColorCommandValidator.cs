namespace Fashionista.Application.ProductColors.Commands.Create
{
    using Fashionista.Domain.Entities;
    using FluentValidation;

    using static Validation.Constants;

    public class CreateProductColorCommandValidator : AbstractValidator<ProductColor>
    {
        public CreateProductColorCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .Length(NameMinLength, NameMaxLength)
                .WithMessage(string.Format(NameLengthMsg, NameMinLength, NameMaxLength));
        }
    }
}

namespace Fashionista.Application.ProductSizes.Commands.Create
{
    using Fashionista.Domain.Entities;
    using FluentValidation;

    using static Validation.Constants;

    public class CreateProductSizeCommandValidation : AbstractValidator<ProductSize>
    {
        public CreateProductSizeCommandValidation()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .Length(NameMinLength, NameMaxLength)
                .WithMessage(string.Format(NameLengthMsg, NameMinLength, NameMaxLength));
        }
    }
}

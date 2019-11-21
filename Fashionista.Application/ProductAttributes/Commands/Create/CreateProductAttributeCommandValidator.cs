namespace Fashionista.Application.ProductAttributes.Commands.Create
{
    using Fashionista.Domain.Entities;
    using FluentValidation;

    public class CreateProductAttributeCommandValidator : AbstractValidator<ProductAttributes>
    {
        public CreateProductAttributeCommandValidator()
        {
            this.RuleFor(x => x.Quantity)
                .NotEmpty()
                .GreaterThan(0);

            this.RuleFor(x => x.ProductColorId)
                .NotEmpty()
                .GreaterThan(0);

            this.RuleFor(x => x.ProductSizeId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}

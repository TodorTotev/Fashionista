namespace Fashionista.Application.Products.Commands.Edit
{
    using Fashionista.Domain.Entities;
    using FluentValidation;

    using static Validation.Constants;

    public class EditProductCommandValidator : AbstractValidator<Product>
    {
        public EditProductCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .Length(NameMinLength, NameMaxLength)
                .WithMessage(string.Format(NameLengthMsg, NameMinLength, NameMaxLength));

            this.RuleFor(x => x.Description)
                .NotEmpty()
                .Length(DescMinLength, DescMaxLength)
                .WithMessage(string.Format(DescLengthMsg, DescMinLength, DescMaxLength));

            this.RuleFor(x => x.BrandId)
                .NotEmpty();

            this.RuleFor(x => x.Price)
                .NotEmpty();

            this.RuleFor(x => x.SubCategoryId)
                .NotEmpty();

            this.RuleFor(x => x.ProductType)
                .NotEmpty();
        }
    }
}

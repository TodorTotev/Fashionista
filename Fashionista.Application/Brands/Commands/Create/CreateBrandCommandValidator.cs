namespace Fashionista.Application.Brands.Commands.Create
{
    using FluentValidation;
    using Humanizer;

    using static Validation.Constants;

    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .Length(NameMinLength, NameMaxLength)
                .WithMessage(NameLengthMessage.FormatWith(NameMinLength, NameMaxLength));

            this.RuleFor(x => x.Photo)
                .NotEmpty();
        }
    }
}

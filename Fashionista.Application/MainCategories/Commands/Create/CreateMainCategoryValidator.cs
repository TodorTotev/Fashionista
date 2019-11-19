namespace Fashionista.Application.MainCategories.Commands.Create
{
    using FluentValidation;

    using static Validation.Constants;

    public class CreateMainCategoryValidator : AbstractValidator<CreateMainCategoryCommand>
    {
        public CreateMainCategoryValidator()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .Length(NameMinLength, NameMaxLength)
                .WithMessage(string.Format(NameLengthMessage, NameMinLength, NameMaxLength));
        }
    }
}

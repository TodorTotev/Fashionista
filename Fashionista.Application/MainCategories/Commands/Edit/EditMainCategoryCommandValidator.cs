namespace Fashionista.Application.MainCategories.Commands.Edit
{
    using FluentValidation;
    using Humanizer;

    using static Validation.Constants;

    public class EditMainCategoryCommandValidator : AbstractValidator<EditMainCategoryCommand>
    {
        public EditMainCategoryCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .Length(NameMinLength, NameMaxLength)
                .WithMessage(NameLengthMessage.FormatWith(NameMinLength, NameMaxLength));
        }
    }
}

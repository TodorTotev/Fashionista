namespace Fashionista.Application.SubCategories.Commands.Edit
{
    using FluentValidation;

    using static Validation.Constants;

    public class EditSubCategoryCommandValidator : AbstractValidator<EditSubCategoryCommand>
    {
        public EditSubCategoryCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .Length(MinNameLength, MaxNameLength)
                .WithMessage(string.Format(NameLengthMessage, MinNameLength, MaxNameLength));

            this.RuleFor(x => x.Description)
                .NotEmpty()
                .Length(MinDescLength, MaxDescLength)
                .WithMessage(string.Format(DescLengthMessage, MinDescLength, MaxDescLength));
        }
    }
}

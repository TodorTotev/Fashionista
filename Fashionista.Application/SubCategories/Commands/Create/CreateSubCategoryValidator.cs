namespace Fashionista.Application.SubCategories.Commands.Create
{
    using FluentValidation;

    using static Validation.Constants;

    public class CreateSubCategoryValidator : AbstractValidator<CreateSubCategoryCommand>
    {
        public CreateSubCategoryValidator()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .Length(MinNameLength, MaxNameLength)
                .WithMessage(string.Format(NameLengthMessage, MinNameLength, MaxNameLength));

            this.RuleFor(x => x.Description)
                .NotEmpty()
                .Length(MinDescLength, MaxDescLength)
                .WithMessage(string.Format(DescLengthMessage, MinDescLength, MaxDescLength));

            this.RuleFor(x => x.MainCategoryId)
                .NotEmpty();
        }
    }
}

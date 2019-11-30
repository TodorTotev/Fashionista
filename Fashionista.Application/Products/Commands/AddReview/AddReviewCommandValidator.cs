namespace Fashionista.Application.Products.Commands.AddReview
{
    using FluentValidation;

    public class AddReviewCommandValidator : AbstractValidator<AddReviewCommand>
    {
        public AddReviewCommandValidator()
        {
            this.RuleFor(x => x.Rating)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(5);
        }
    }
}

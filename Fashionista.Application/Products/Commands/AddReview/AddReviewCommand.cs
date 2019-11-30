namespace Fashionista.Application.Products.Commands.AddReview
{
    using MediatR;

    public class AddReviewCommand : IRequest<int>
    {
        public int Id { get; set; }

        public int Rating { get; set; }
    }
}
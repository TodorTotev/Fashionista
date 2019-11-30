namespace Fashionista.Application.Products.Commands.AddReview
{
    using MediatR;

    public class AddReviewCommand : IRequest<int>
    {
        public int ProductId { get; set; }

        public int Rating { get; set; }
    }
}
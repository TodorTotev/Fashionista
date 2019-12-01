namespace Fashionista.Application.Wishlist.Commands.Delete
{
    using MediatR;

    public class DeleteFavoriteProductCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}

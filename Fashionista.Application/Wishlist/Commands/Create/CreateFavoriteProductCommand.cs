namespace Fashionista.Application.Wishlist.Commands.Create
{
    using MediatR;

    public class CreateFavoriteProductCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}

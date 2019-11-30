namespace Fashionista.Application.Wishlist.Commands.Create
{
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateFavoriteProductCommand : IRequest<int>
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }
    }
}

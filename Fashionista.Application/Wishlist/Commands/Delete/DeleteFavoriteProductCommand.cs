namespace Fashionista.Application.Wishlist.Commands.Delete
{
    using Fashionista.Domain.Entities;
    using MediatR;

    public class DeleteFavoriteProductCommand : IRequest<int>
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }
    }
}

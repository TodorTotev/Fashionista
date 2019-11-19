namespace Fashionista.Domain.Entities
{
    public class FavoriteProduct
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}

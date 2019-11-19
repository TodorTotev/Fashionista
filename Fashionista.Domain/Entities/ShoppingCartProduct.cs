namespace Fashionista.Domain.Entities
{
    public class ShoppingCartProduct
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public int Quantity { get; set; }
    }
}

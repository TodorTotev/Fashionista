namespace Fashionista.Domain.Entities
{
    using FashionNova.Data.Models;

    public class OrderProduct
    {
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}

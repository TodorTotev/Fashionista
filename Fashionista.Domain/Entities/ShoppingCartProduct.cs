namespace Fashionista.Domain.Entities
{
    using System;

    using Fashionista.Domain.Infrastructure;

    public class ShoppingCartProduct : IDeletableEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public int ColorId { get; set; }

        public int SizeId { get; set; }

        public int Quantity { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

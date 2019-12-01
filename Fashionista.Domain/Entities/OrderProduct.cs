namespace Fashionista.Domain.Entities
{
    using System;

    using Fashionista.Domain.Infrastructure;

    public class OrderProduct : IDeletableEntity
    {
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

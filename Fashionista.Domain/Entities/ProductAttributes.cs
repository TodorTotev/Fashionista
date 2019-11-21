namespace Fashionista.Domain.Entities
{
    using Fashionista.Domain.Infrastructure;

    public class ProductAttributes : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public int ProductColorId { get; set; }

        public virtual ProductColor ProductColor { get; set; }

        public int ProductSizeId { get; set; }

        public virtual ProductSize ProductSize { get; set; }

        public int Quantity { get; set; }
    }
}

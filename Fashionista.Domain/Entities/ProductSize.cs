namespace Fashionista.Domain.Entities
{
    using Fashionista.Domain.Infrastructure;

    public class ProductSize : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
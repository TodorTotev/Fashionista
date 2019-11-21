namespace Fashionista.Domain.Entities
{
    using Fashionista.Domain.Infrastructure;

    public class ProductColor : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
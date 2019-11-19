namespace Fashionista.Domain.Entities
{
    using System.Collections.Generic;

    using Fashionista.Domain.Infrastructure;

    public class SubCategory : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MainCategoryId { get; set; }

        public MainCategory MainCategory { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

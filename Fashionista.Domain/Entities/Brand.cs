namespace Fashionista.Domain.Entities
{
    using System.Collections.Generic;

    using Fashionista.Domain.Infrastructure;

    public class Brand : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string BrandPhotoUrl { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

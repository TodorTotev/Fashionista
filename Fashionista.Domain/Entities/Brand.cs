namespace Fashionista.Domain.Entities
{
    using System.Collections.Generic;

    using Fashionista.Domain.Infrastructure;
    using FashionNova.Data.Models;

    public class Brand : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

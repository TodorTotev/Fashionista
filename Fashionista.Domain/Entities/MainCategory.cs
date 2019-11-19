// ReSharper disable CollectionNeverUpdated.Global
namespace Fashionista.Domain.Entities
{
    using System.Collections.Generic;

    using Fashionista.Domain.Infrastructure;

    public class MainCategory : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}

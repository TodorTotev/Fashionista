namespace Fashionista.Domain.Entities
{
    using System.Collections.Generic;

    using Fashionista.Domain.Infrastructure;

    public class MainCategory : BaseDeletableModel<int>
    {
        public MainCategory()
        {
            this.SubCategories = new List<SubCategory>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }
    }
}

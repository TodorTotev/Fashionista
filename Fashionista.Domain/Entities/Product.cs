namespace Fashionista.Domain.Entities
{
    using System.Collections.Generic;

    using Fashionista.Domain.Entities.Enums;
    using Fashionista.Domain.Infrastructure;

    public class Product : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsHidden { get; set; }

        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public ProductSize ProductSize { get; set; }

        public ProductType ProductType { get; set; }

        public ProductColor ProductColor { get; set; }

        public virtual ICollection<string> Photos { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; } = new List<ShoppingCartProduct>();

        public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; } = new List<FavoriteProduct>();
    }
}

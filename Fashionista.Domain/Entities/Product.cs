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

        public ProductTypes ProductType { get; set; }

        public ProductColors ProductColor { get; set; }

        public virtual ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; } = new List<ShoppingCartProduct>();

        public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; } = new List<FavoriteProduct>();

        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
namespace Fashionista.Persistence.Configurations
{
    using System.Collections.Generic;

    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public static class ProductConfiguration
    {
        public static void Configure(this ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasOne(x => x.SubCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .Property(x => x.Photos)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v));

            builder.Entity<FavoriteProduct>()
                .HasKey(x => new { x.ProductId, x.ApplicationUserId });

            builder.Entity<OrderProduct>().HasKey(x => new { x.OrderId, x.ProductId });
        }
    }
}

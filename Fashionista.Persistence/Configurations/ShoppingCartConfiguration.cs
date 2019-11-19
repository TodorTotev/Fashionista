namespace Fashionista.Persistence.Configurations
{
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public static class ShoppingCartConfiguration
    {
        public static void Configure(this ModelBuilder builder)
        {
            builder.Entity<ShoppingCart>()
                .HasOne(x => x.User)
                .WithOne(x => x.ShoppingCart)
                .HasForeignKey<ApplicationUser>(x => x.ShoppingCartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ShoppingCartProduct>()
                .HasKey(x => new { x.ProductId, x.ShoppingCartId });
        }
    }
}

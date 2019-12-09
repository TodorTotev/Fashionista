namespace Fashionista.Persistence.Configurations
{
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public static class NotificationConfiguration
    {
        public static void Configure(this ModelBuilder builder)
        {
            builder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(p => p.Notifications)
                .HasForeignKey(n => n.ApplicationUserId);

            builder.Entity<Notification>()
                .Property(n => n.Content)
                .IsUnicode()
                .HasMaxLength(1024);

            builder.Entity<Notification>()
                .Property(n => n.Header)
                .IsUnicode()
                .HasMaxLength(1024);
        }
    }
}

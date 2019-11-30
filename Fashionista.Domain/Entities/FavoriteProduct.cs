namespace Fashionista.Domain.Entities
{
    using System;

    using Fashionista.Domain.Infrastructure;

    public class FavoriteProduct : IDeletableEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

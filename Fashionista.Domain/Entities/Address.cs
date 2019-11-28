namespace Fashionista.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using Fashionista.Domain.Infrastructure;

    public class Address : IDeletableEntity
    {
        public Address()
        {
            this.Addresses = new HashSet<Order>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public ICollection<Order> Addresses { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

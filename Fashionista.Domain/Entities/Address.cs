namespace Fashionista.Domain.Entities
{
    using System.Collections.Generic;

    public class Address
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

        public virtual ApplicationUser FashionNovaUser { get; set; }

        public ICollection<Order> Addresses { get; set; }
    }
}

namespace Fashionista.Domain.Entities
{
    using System.Collections.Generic;

    using Fashionista.Domain.Infrastructure;

    public class City : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Postcode { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}

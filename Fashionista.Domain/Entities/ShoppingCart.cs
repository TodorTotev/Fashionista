namespace Fashionista.Domain.Entities
{
    using System.Collections.Generic;

    using Fashionista.Domain.Infrastructure;

    public class ShoppingCart : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; } = new List<ShoppingCartProduct>();
    }
}

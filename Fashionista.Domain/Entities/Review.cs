namespace Fashionista.Domain.Entities
{
    using Fashionista.Domain.Infrastructure;
    using FashionNova.Data.Models;

    public class Review : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public Product Product { get; set; }
    }
}

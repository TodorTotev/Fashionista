namespace Fashionista.Domain.Entities
{
    using Fashionista.Domain.Infrastructure;

    public class Photo : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public string PhotoUrl { get; set; }

        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}

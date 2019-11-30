namespace Fashionista.Application.Common.Models
{
    using System.Linq;

    using AutoMapper;
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;

    public class WishlistProductLookup : IMapFrom<FavoriteProduct>, IHaveCustomMapping
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductPhotoUrl { get; set; }

        public decimal ProductPrice { get; set; }

        public string ApplicationUserId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<FavoriteProduct, WishlistProductLookup>()
                .ForMember(
                    x => x.ProductPhotoUrl,
                    y => y.MapFrom(
                        src => src.Product.Photos.FirstOrDefault()));
        }
    }
}

using AutoMapper;

namespace Fashionista.Application.Common.Models
{
    using System.Collections.Generic;

    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;

    public class CartProductLookupModel : IMapFrom<ShoppingCartProduct>, IHaveCustomMappings
    {
        private decimal totalPrice;

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int ColorId { get; set; }

        public string ColorName { get; set; }

        public int SizeId { get; set; }

        public string SizeName { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice
        {
            get => this.ProductPrice * this.Quantity;
            set => this.totalPrice = value;
        }

        public ICollection<string> ProductPhotos { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ShoppingCartProduct, CartProductLookupModel>()
                .ForMember(
                    x => x.ColorId,
                    y => y.MapFrom(
                        src => src.ColorId));

            configuration.CreateMap<ShoppingCartProduct, CartProductLookupModel>()
                .ForMember(
                    x => x.SizeId,
                    y => y.MapFrom(
                        src => src.SizeId));
        }
    }
}

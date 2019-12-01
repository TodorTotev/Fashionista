namespace Fashionista.Application.Common.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;

    public class CartProductLookupModel : IMapFrom<ShoppingCartProduct>
    {
        private decimal totalPrice;

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice
        {
            get => this.ProductPrice * this.Quantity;
            set => this.totalPrice = value;
        }

        public ICollection<string> ProductPhotos { get; set; }
    }
}

namespace Fashionista.Application.Common.Models
{
    using System;

    using AutoMapper;
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;

    public class ProductAttributesLookupModel : IMapFrom<ProductAttributes>
    {
        public int Id { get; set; }

        public string ProductColorName { get; set; }

        public string ProductSizeName { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}

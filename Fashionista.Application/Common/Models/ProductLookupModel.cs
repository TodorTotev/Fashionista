namespace Fashionista.Application.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;

    public class ProductLookupModel : IMapFrom<Product>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsHidden { get; set; }

        public Brand Brand { get; set; }

        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public string SubCategoryMainCategoryName { get; set; }

        public int SubCategoryMainCategoryId { get; set; }

        public virtual ICollection<ProductAttributes> ProductAttributes { get; set; }

        public virtual ICollection<string> Photos { get; set; }

        public DateTime CreatedOn { get; set; }

        public ProductType ProductType { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Product, ProductLookupModel>()
                .ForMember(
                    x => x.SubCategoryMainCategoryName,
                    y => y.MapFrom(
                        src => src.SubCategory.MainCategory.Name));

            configuration.CreateMap<Product, ProductLookupModel>()
                .ForMember(
                    x => x.SubCategoryMainCategoryId,
                    y => y.MapFrom(
                        src => src.SubCategory.MainCategoryId));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Fashionista.Domain.Entities.Enums;

namespace Fashionista.Application.Common.Models
{
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;

    public class ProductLookupModel : IMapFrom<Product>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsHidden { get; set; }

        public string BrandName { get; set; }

        public string SubCategoryName { get; set; }

        public virtual ICollection<string> Photos { get; set; }

        public DateTime CreatedOn { get; set; }

        public int AverageRating { get; set; }

        public int ReviewsCount { get; set; }

        public ProductType ProductType { get; set; }

        public ProductColor ProductColor { get; set; }

        public ProductSize ProductSize { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Product, ProductLookupModel>()
                .ForMember(
                    x => x.BrandName,
                    y => y.MapFrom(
                        src => src.Brand.Name));

            configuration.CreateMap<Product, ProductLookupModel>()
                .ForMember(
                    x => x.SubCategoryName,
                    y => y.MapFrom(
                        src => src.Brand.Name));

            configuration.CreateMap<Product, ProductLookupModel>()
                .ForMember(
                    x => x.ReviewsCount,
                    y => y.MapFrom(
                        src => src.Reviews.Count));

            configuration.CreateMap<Product, ProductLookupModel>()
                .ForMember(
                    x => x.AverageRating,
                    y => y.MapFrom(
                        src => src.Reviews.Select(x => x.Rating).Sum() / src.Reviews.Count));
        }
    }
}
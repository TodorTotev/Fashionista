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

        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public string BrandPhotoUrl { get; set; }

        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public string SubCategoryMainCategoryName { get; set; }

        public int SubCategoryMainCategoryId { get; set; }

        public List<ProductAttributes> ProductAttributes { get; set; }

        public virtual ICollection<string> Photos { get; set; }

        public DateTime CreatedOn { get; set; }

        public int AverageRating { get; set; }

        public int ReviewsCount { get; set; }

        public ProductType ProductType { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Product, ProductLookupModel>()
                .ForMember(
                    x => x.BrandName,
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

            configuration.CreateMap<Product, ProductLookupModel>()
                .ForMember(
                    x => x.BrandPhotoUrl,
                    y => y.MapFrom(
                        src => src.Brand.BrandPhotoUrl));
        }
    }
}

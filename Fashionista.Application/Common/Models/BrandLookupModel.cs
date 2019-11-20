namespace Fashionista.Application.Common.Models
{
    using System;

    using AutoMapper;
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;

    public class BrandLookupModel : IMapFrom<Brand>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ProductsCount { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Brand, BrandLookupModel>()
                .ForMember(
                    x => x.ProductsCount,
                    o => o.MapFrom(
                        src => src.Products.Count));
        }
    }
}

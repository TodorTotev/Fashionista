namespace Fashionista.Application.Common.Models
{
    using AutoMapper;
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;

    public class SubCategoryLookupModel : IMapFrom<SubCategory>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MainCategoryName { get; set; }

        public int ProductsCount { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<SubCategory, SubCategoryLookupModel>()
                .ForMember(
                    x => x.ProductsCount,
                    o => o.MapFrom(
                        src => src.Products.Count));
        }
    }
}

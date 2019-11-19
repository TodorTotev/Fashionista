namespace Fashionista.Application.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;

    public class MainCategoryLookupModel : IMapFrom<MainCategory>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime CreatedOn { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<MainCategory, MainCategoryLookupModel>()
                .ForMember(
                    d => d.Count,
                    m => m.MapFrom(c => c.SubCategories.Count));
        }
    }
}

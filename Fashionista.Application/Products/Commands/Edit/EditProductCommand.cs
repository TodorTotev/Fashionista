namespace Fashionista.Application.Products.Commands.Edit
{
    using System.Collections.Generic;

    using AutoMapper;
    using Fashionista.Application.Brands.Queries.GetAllBrandsSelectList;
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Application.SubCategories.Queries.GetAllSubCategoriesSelectList;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class EditProductCommand : IRequest<int>, IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int BrandId { get; set; }

        public decimal Price { get; set; }

        public bool IsHidden { get; set; }

        public int SubCategoryId { get; set; }

        public ICollection<IFormFile> Photos { get; set; }

        public ProductType ProductType { get; set; }

        public GetAllSubCategoriesSelectListViewModel SubCategories { get; set; }

        public GetAllBrandsSelectListViewModel Brands { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, EditProductCommand>()
                .ForMember(x => x.Photos, opt => opt.Ignore());
        }
    }
}

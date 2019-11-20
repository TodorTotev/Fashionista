namespace Fashionista.Application.SubCategories.Commands.Edit
{
    using AutoMapper;
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class EditSubCategoryCommand : IRequest<int>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<SubCategory, EditSubCategoryCommand>();
        }
    }
}

namespace Fashionista.Application.MainCategories.Commands.Edit
{
    using AutoMapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class EditMainCategoryCommand : IRequest<int>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<MainCategory, EditMainCategoryCommand>()
                .ForMember(
                    x => x.Name,
                    o => o.MapFrom(src => src.Name));
        }
    }
}

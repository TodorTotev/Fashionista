namespace Fashionista.Application.MainCategories.Commands.Create
{
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateMainCategoryCommand : IRequest<int>, IMapTo<MainCategory>
    {
        public string Name { get; set; }
    }
}

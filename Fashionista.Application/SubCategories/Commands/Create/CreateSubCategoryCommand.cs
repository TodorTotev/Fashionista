namespace Fashionista.Application.SubCategories.Commands.Create
{
    using System.Collections.Generic;
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Application.MainCategories.Queries.GetAllMainCategoriesSelectList;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateSubCategoryCommand : IRequest<int>, IMapTo<SubCategory>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int MainCategoryId { get; set; }

        public IEnumerable<GetAllMainCategoriesSelectListQuery> AllMainCategoriesSelectList { get; set; }
    }
}

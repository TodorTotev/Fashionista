namespace Fashionista.Application.SubCategories.Commands.Create
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateSubCategoryCommand : IRequest<int>, IMapTo<SubCategory>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int MainCategoryId { get; set; }

        public IEnumerable<MainCategoryLookupModel> AllMainCategoriesSelectList { get; set; }
    }
}

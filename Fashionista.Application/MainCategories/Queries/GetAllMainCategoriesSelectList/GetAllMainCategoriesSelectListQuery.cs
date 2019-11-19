namespace Fashionista.Application.MainCategories.Queries.GetAllMainCategoriesSelectList
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;
    using MediatR;

    public class GetAllMainCategoriesSelectListQuery : IRequest<IEnumerable<MainCategoryLookupModel>>
    {
    }
}

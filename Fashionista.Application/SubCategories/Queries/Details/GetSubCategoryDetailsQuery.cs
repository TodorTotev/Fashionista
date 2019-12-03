namespace Fashionista.Application.SubCategories.Queries.Details
{
    using Fashionista.Application.Common.Models;
    using MediatR;

    public class GetSubCategoryDetailsQuery : IRequest<SubCategoryLookupModel>
    {
        public int Id { get; set; }
    }
}

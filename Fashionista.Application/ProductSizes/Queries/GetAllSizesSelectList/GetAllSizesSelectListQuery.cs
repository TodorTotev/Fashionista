namespace Fashionista.Application.ProductSizes.Queries.GetAllSizesSelectList
{
    using MediatR;

    public class GetAllSizesSelectListQuery : IRequest<GetAllSizesSelectListViewModel>
    {
        public int? MainCategoryId { get; set; }
    }
}

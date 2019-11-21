namespace Fashionista.Application.Products.Queries.GetAllProductsPaged
{
    using MediatR;

    public class GetAllProductsPagedQuery : IRequest<GetAllProductsPagedViewModel>
    {
        // If you want to select only the active products, set this property to true.
        public bool IsActive { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}

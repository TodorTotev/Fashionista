namespace Fashionista.Application.Brands.Queries.Queries
{
    using MediatR;

    public class GetAllBrandsPagedQuery : IRequest<GetAllBrandsPagedViewModel>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}

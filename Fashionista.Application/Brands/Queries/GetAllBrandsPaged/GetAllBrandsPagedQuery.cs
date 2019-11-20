namespace Fashionista.Application.Brands.Queries.GetAllBrandsPaged
{
    using MediatR;

    public class GetAllBrandsPagedQuery : IRequest<GetAllBrandsPagedViewModel>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}

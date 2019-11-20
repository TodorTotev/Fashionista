using MediatR;

namespace Fashionista.Application.Brands.Queries.Queries
{
    public class GetAllBrandsPagedQuery : IRequest<GetAllBrandsPagedViewModel>
    {
        public int PageNumber { get; set; }
        
        public int PageSize { get; set; }
    }
}
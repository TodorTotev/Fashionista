using Microsoft.EntityFrameworkCore;

namespace Fashionista.Application.Brands.Queries.GetAllBrandsPaged
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class GetAllBrandsPagedQueryHandler : IRequestHandler<GetAllBrandsPagedQuery, GetAllBrandsPagedViewModel>
    {
        private readonly IDeletableEntityRepository<Brand> brandsRepository;

        public GetAllBrandsPagedQueryHandler(
            IDeletableEntityRepository<Brand> brandsRepository)
        {
            this.brandsRepository = brandsRepository;
        }

        public async Task<GetAllBrandsPagedViewModel> Handle(GetAllBrandsPagedQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var currentPageEntities = await this.brandsRepository
                .AllAsNoTracking()
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .To<BrandLookupModel>()
                .ToListAsync(cancellationToken);

            return new GetAllBrandsPagedViewModel
            {
                Brands = currentPageEntities,
            };
        }
    }
}

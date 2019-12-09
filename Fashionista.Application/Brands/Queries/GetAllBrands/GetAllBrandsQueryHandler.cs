namespace Fashionista.Application.Brands.Queries.GetAllBrands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, GetAllBrandsViewModel>
    {
        private readonly IDeletableEntityRepository<Brand> brandsRepository;

        public GetAllBrandsQueryHandler(
            IDeletableEntityRepository<Brand> brandsRepository)
        {
            this.brandsRepository = brandsRepository;
        }

        public async Task<GetAllBrandsViewModel> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var currentPageEntities = await this.brandsRepository
                .AllAsNoTracking()
                .To<BrandLookupModel>()
                .ToListAsync(cancellationToken);

            return new GetAllBrandsViewModel()
            {
                Brands = currentPageEntities,
            };
        }
    }
}

namespace Fashionista.Application.Brands.Queries.GetAllBrands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, GetAllBrandsViewModel>
    {
        private readonly IDeletableEntityRepository<Brand> brandsRepository;
        private readonly IMapper mapper;

        public GetAllBrandsQueryHandler(
            IDeletableEntityRepository<Brand> brandsRepository,
            IMapper mapper)
        {
            this.brandsRepository = brandsRepository;
            this.mapper = mapper;
        }

        public async Task<GetAllBrandsViewModel> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var currentPageEntities = await this.brandsRepository
                .AllAsNoTracking()
                .ProjectTo<BrandLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetAllBrandsViewModel()
            {
                Brands = currentPageEntities,
            };
        }
    }
}

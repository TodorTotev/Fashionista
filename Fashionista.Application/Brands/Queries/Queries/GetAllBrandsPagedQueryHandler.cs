namespace Fashionista.Application.Brands.Queries.Queries
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
    using X.PagedList;

    public class GetAllBrandsPagedQueryHandler : IRequestHandler<GetAllBrandsPagedQuery, GetAllBrandsPagedViewModel>
    {
        private readonly IDeletableEntityRepository<Brand> brandsRepository;
        private readonly IMapper mapper;

        public GetAllBrandsPagedQueryHandler(
            IDeletableEntityRepository<Brand> brandsRepository,
            IMapper mapper)
        {
            this.brandsRepository = brandsRepository;
            this.mapper = mapper;
        }

        public async Task<GetAllBrandsPagedViewModel> Handle(GetAllBrandsPagedQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var currentPageEntities = await this.brandsRepository
                .AllAsNoTracking()
                .ProjectTo<BrandLookupModel>(this.mapper.ConfigurationProvider)
                .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);

            return new GetAllBrandsPagedViewModel
            {
                Brands = currentPageEntities,
            };
        }
    }
}

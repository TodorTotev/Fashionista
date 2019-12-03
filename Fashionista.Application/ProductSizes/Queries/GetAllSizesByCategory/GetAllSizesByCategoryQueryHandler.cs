using Fashionista.Application.Infrastructure;

namespace Fashionista.Application.ProductSizes.Queries.GetAllSizesByCategory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAllSizesByCategoryQueryHandler :
        IRequestHandler<GetAllSizesByCategoryQuery, List<ProductSizeLookupModel>>
    {
        private readonly IDeletableEntityRepository<ProductSize> productSizesRepository;
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;
        private readonly IMapper mapper;

        public GetAllSizesByCategoryQueryHandler(
            IDeletableEntityRepository<ProductSize> productSizesRepository,
            IDeletableEntityRepository<MainCategory> mainCategoryRepository,
            IMapper mapper)
        {
            this.productSizesRepository = productSizesRepository;
            this.mainCategoryRepository = mainCategoryRepository;
            this.mapper = mapper;
        }

        public async Task<List<ProductSizeLookupModel>> Handle(
            GetAllSizesByCategoryQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CheckIfCategoryExists(this.mainCategoryRepository, request.Id))
            {
                throw new NotFoundException(nameof(MainCategory), request.Id);
            }

            var sizes = await this.productSizesRepository
                .All()
                .Where(x => x.MainCategoryId == request.Id)
                .ProjectTo<ProductSizeLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return sizes;
        }

        private static async Task<bool> CheckIfCategoryExists(
            IDeletableEntityRepository<MainCategory> mainCategoryRepository, int id) =>
            await mainCategoryRepository
                .All()
                .AnyAsync(x => x.Id == id);
    }
}

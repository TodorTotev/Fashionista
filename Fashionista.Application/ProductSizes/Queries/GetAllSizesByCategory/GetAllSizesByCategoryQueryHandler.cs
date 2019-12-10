namespace Fashionista.Application.ProductSizes.Queries.GetAllSizesByCategory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAllSizesByCategoryQueryHandler :
        IRequestHandler<GetAllSizesByCategoryQuery, List<ProductSizeLookupModel>>
    {
        private readonly IDeletableEntityRepository<ProductSize> productSizesRepository;
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;

        public GetAllSizesByCategoryQueryHandler(
            IDeletableEntityRepository<ProductSize> productSizesRepository,
            IDeletableEntityRepository<MainCategory> mainCategoryRepository)
        {
            this.productSizesRepository = productSizesRepository;
            this.mainCategoryRepository = mainCategoryRepository;
        }

        public async Task<List<ProductSizeLookupModel>> Handle(
            GetAllSizesByCategoryQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await this.CheckIfCategoryExists(request.Id))
            {
                throw new NotFoundException(nameof(MainCategory), request.Id);
            }

            var sizes = await this.productSizesRepository
                .All()
                .Where(x => x.MainCategoryId == request.Id)
                .To<ProductSizeLookupModel>()
                .ToListAsync(cancellationToken);

            return sizes;
        }

        private async Task<bool> CheckIfCategoryExists(int id) =>
            await this.mainCategoryRepository
                .All()
                .AnyAsync(x => x.Id == id);
    }
}

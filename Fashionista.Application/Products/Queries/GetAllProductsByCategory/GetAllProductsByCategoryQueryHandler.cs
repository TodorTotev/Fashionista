namespace Fashionista.Application.Products.Queries.GetAllProductsByCategory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAllProductsByCategoryQueryHandler :
        IRequestHandler<GetAllProductsByCategoryQuery, List<ProductLookupModel>>
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public GetAllProductsByCategoryQueryHandler(
            IDeletableEntityRepository<Product> productsRepository,
            IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.productsRepository = productsRepository;
            this.subCategoryRepository = subCategoryRepository;
        }

        public async Task<List<ProductLookupModel>> Handle(GetAllProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckAssistant.CheckIfSubCategoryExists(request.Id, this.subCategoryRepository))
            {
                throw new NotFoundException(nameof(SubCategory), request.Id);
            }

            var products = await this.productsRepository
                .AllAsNoTracking()
                .Where(x => x.SubCategoryId == request.Id
                            && x.ProductAttributes.Any())
                .To<ProductLookupModel>()
                .ToListAsync(cancellationToken);

            return products;
        }
    }
}

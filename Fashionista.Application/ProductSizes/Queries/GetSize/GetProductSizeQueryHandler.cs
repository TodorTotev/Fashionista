namespace Fashionista.Application.ProductSizes.Queries.GetSize
{
    using System;
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

    public class GetProductSizeQueryHandler : IRequestHandler<GetProductSizeQuery, ProductSizeLookupModel>
    {
        private readonly IDeletableEntityRepository<ProductSize> productSizesRepository;

        public GetProductSizeQueryHandler(
            IDeletableEntityRepository<ProductSize> productSizesRepository)
        {
            this.productSizesRepository = productSizesRepository;
        }

        public async Task<ProductSizeLookupModel> Handle(GetProductSizeQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckAssistant.CheckIfProductSizeExists(request.Id, this.productSizesRepository))
            {
                throw new NotFoundException(nameof(ProductSize), "Size not found");
            }

            return await this.productSizesRepository
                .AllAsNoTracking()
                .To<ProductSizeLookupModel>()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}

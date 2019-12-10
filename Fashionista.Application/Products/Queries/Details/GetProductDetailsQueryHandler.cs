namespace Fashionista.Application.Products.Queries.Details
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, GetProductDetailsViewModel>
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public GetProductDetailsQueryHandler(
            IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<GetProductDetailsViewModel> Handle(
            GetProductDetailsQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var requestedEntity = await this.productRepository
                                      .AllAsNoTracking()
                                      .To<ProductLookupModel>()
                                      .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                  ?? throw new NotFoundException(nameof(Product), request.Id);

            return new GetProductDetailsViewModel
            {
                Product = requestedEntity,
            };
        }
    }
}

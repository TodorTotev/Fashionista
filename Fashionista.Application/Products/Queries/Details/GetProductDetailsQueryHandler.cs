using System.Linq;

namespace Fashionista.Application.Products.Queries.Details
{
    using System;
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

    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, GetProductDetailsViewModel>
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IDeletableEntityRepository<ProductAttributes> productAttributesRepository;
        private readonly IMapper mapper;

        public GetProductDetailsQueryHandler(
            IDeletableEntityRepository<Product> productRepository,
            IDeletableEntityRepository<ProductAttributes> productAttributesRepository,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.productAttributesRepository = productAttributesRepository;
            this.mapper = mapper;
        }

        public async Task<GetProductDetailsViewModel> Handle(
            GetProductDetailsQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var requestedEntity = await this.productRepository
                                      .AllAsNoTracking()
                                      .ProjectTo<ProductLookupModel>(this.mapper.ConfigurationProvider)
                                      .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                  ?? throw new NotFoundException(nameof(Product), request.Id);

            return new GetProductDetailsViewModel
            {
                Product = requestedEntity,
            };
        }
    }
}

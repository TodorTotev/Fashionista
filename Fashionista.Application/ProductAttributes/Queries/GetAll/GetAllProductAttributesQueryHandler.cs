namespace Fashionista.Application.ProductAttributes.Queries.GetAll
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Humanizer;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class
        GetAllProductAttributesQueryHandler : IRequestHandler<GetAllProductAttributesQuery,
            GetAllProductAttributesViewModel>
    {
        private readonly IDeletableEntityRepository<ProductAttributes> productAttributesRepository;
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IMapper mapper;

        public GetAllProductAttributesQueryHandler(
            IDeletableEntityRepository<ProductAttributes> productAttributesRepository,
            IDeletableEntityRepository<Product> productsRepository,
            IMapper mapper)
        {
            this.productAttributesRepository = productAttributesRepository;
            this.productsRepository = productsRepository;
            this.mapper = mapper;
        }

        public async Task<GetAllProductAttributesViewModel> Handle(
            GetAllProductAttributesQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckAssistant.CheckIfProductExists(request.Id, this.productsRepository))
            {
                throw new NotFoundException(nameof(Product), "with id {0}".FormatWith(request.Id));
            }

            var requestedEntities = await this.productAttributesRepository
                .AllAsNoTracking()
                .Where(x => x.ProductId == request.Id)
                .ProjectTo<ProductAttributesLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var product = await this.productsRepository
                .AllAsNoTracking()
                .FirstAsync(x => x.Id == request.Id, cancellationToken);

            return new GetAllProductAttributesViewModel
            {
                ProductAttributesList = requestedEntities,
                ProductName = product.Name,
                ProductId = request.Id,
            };
        }
    }
}

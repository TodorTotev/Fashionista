namespace Fashionista.Application.ProductAttributes.Queries.GetAll
{
    using System;
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

    public class
        GetAllProductAttributesQueryHandler : IRequestHandler<GetAllProductAttributesQuery,
            GetAllProductAttributesViewModel>
    {
        private readonly IDeletableEntityRepository<ProductAttributes> productAttributesRepository;
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public GetAllProductAttributesQueryHandler(
            IDeletableEntityRepository<ProductAttributes> productAttributesRepository,
            IDeletableEntityRepository<Product> productsRepository)
        {
            this.productAttributesRepository = productAttributesRepository;
            this.productsRepository = productsRepository;
        }

        public async Task<GetAllProductAttributesViewModel> Handle(
            GetAllProductAttributesQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckAssistant.CheckIfProductExists(request.Id, this.productsRepository))
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            var requestedEntities = await this.productAttributesRepository
                .AllAsNoTracking()
                .Where(x => x.ProductId == request.Id)
                .To<ProductAttributesLookupModel>()
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

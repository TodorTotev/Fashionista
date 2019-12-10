namespace Fashionista.Application.ProductAttributes.Queries.GetColorsAndSizes
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

    public class GetColorsAndSizesQueryHandler : IRequestHandler<GetColorsAndSizesQuery, ProductColorsAndSizesViewModel>
    {
        private readonly IDeletableEntityRepository<ProductAttributes> productAttributesRepository;
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public GetColorsAndSizesQueryHandler(
            IDeletableEntityRepository<ProductAttributes> productAttributesRepository,
            IDeletableEntityRepository<Product> productsRepository)
        {
            this.productAttributesRepository = productAttributesRepository;
            this.productsRepository = productsRepository;
        }

        public async Task<ProductColorsAndSizesViewModel> Handle(GetColorsAndSizesQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckAssistant.CheckIfProductExists(request.Id, this.productsRepository))
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            var colors = await this.productAttributesRepository
                .All()
                .Where(x => x.ProductId == request.Id)
                .Select(x => x.ProductColor)
                .To<ProductColorLookupModel>()
                .ToListAsync(cancellationToken);

            var sizes = await this.productAttributesRepository
                .All()
                .Where(x => x.ProductId == request.Id)
                .Select(x => x.ProductSize)
                .To<ProductSizeLookupModel>()
                .ToListAsync(cancellationToken);

            return new ProductColorsAndSizesViewModel
            {
                Sizes = sizes,
                Colors = colors,
            };
        }
    }
}

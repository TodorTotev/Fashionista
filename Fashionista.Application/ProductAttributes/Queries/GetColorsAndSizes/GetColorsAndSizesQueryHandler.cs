namespace Fashionista.Application.ProductAttributes.Queries.GetColorsAndSizes
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
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetColorsAndSizesQueryHandler : IRequestHandler<GetColorsAndSizesQuery, ProductColorsAndSizesViewModel>
    {
        private readonly IDeletableEntityRepository<ProductAttributes> productAttributesRepository;
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IMapper mapper;

        public GetColorsAndSizesQueryHandler(
            IDeletableEntityRepository<ProductAttributes> productAttributesRepository,
            IDeletableEntityRepository<Product> productsRepository,
            IMapper mapper)
        {
            this.productAttributesRepository = productAttributesRepository;
            this.productsRepository = productsRepository;
            this.mapper = mapper;
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
                .ProjectTo<ProductColorLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var sizes = await this.productAttributesRepository
                .All()
                .Where(x => x.ProductId == request.Id)
                .Select(x => x.ProductSize)
                .ProjectTo<ProductSizeLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ProductColorsAndSizesViewModel
            {
                Sizes = sizes,
                Colors = colors,
            };
        }
    }
}
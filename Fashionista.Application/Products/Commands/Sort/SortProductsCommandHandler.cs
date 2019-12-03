namespace Fashionista.Application.Products.Commands.Sort
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;

    public class SortProductsCommandHandler : IRequestHandler<SortProductsCommand, List<ProductLookupModel>>
    {
        private readonly IDeletableEntityRepository<ProductSize> sizesRepository;
        private readonly IDeletableEntityRepository<ProductColor> colorsRepository;

        public SortProductsCommandHandler(
            IDeletableEntityRepository<ProductSize> sizesRepository,
            IDeletableEntityRepository<ProductColor> colorsRepository)
        {
            this.sizesRepository = sizesRepository;
            this.colorsRepository = colorsRepository;
        }

        public async Task<List<ProductLookupModel>> Handle(
            SortProductsCommand request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException();

            if (!await CommonCheckAssistant.CheckIfProductSizeExists(request.SizeId, this.sizesRepository))
            {
                throw new NotFoundException(nameof(ProductSize), request.SizeId);
            }

            if (!await CommonCheckAssistant.CheckIfProductColorExists(request.ColorId, this.colorsRepository))
            {
                throw new NotFoundException(nameof(ProductColor), request.ColorId);
            }

            var products = SortByGender(request.Products, request.Gender);
            products = SortByBrand(products, request.BrandId);
            products = SortByColor(products, request.ColorId);
            products = SortBySize(products, request.SizeId);
            products = ViewSort(products, request.Sort);

            return products.ToList();
        }

        private static IEnumerable<ProductLookupModel> SortByGender(
            IEnumerable<ProductLookupModel> products, ProductType? productType)
        {
            if (productType != null)
            {
                if (productType is ProductType.Men)
                {
                    products = products
                        .Where(x => x.ProductType == ProductType.Men);
                }
                else if (productType is ProductType.Women)
                {
                    products = products
                        .Where(x => x.ProductType == ProductType.Women);
                }
            }

            return products.ToList();
        }

        private static IEnumerable<ProductLookupModel> SortByBrand(
            IEnumerable<ProductLookupModel> products, int? brandId)
        {
            if (brandId == 1000 || brandId == null)
            {
                return products;
            }

            return products
                .Where(x => x.Brand.Id == brandId)
                .ToList();
        }

        private static IEnumerable<ProductLookupModel> SortByColor(
            IEnumerable<ProductLookupModel> products, int colorId) =>
            products
                .Where(x => x.ProductAttributes.Any(y => y.ProductColorId == colorId))
                .ToList();

        private static IEnumerable<ProductLookupModel> SortBySize(
            IEnumerable<ProductLookupModel> products, int sizeId)
        {
            return products
                .Where(x => x.ProductAttributes.Any(y => y.ProductSizeId == sizeId))
                .ToList();
        }

        [SuppressMessage("ReSharper", "SA1137")]
        [SuppressMessage("ReSharper", "SA1107")]
        private static IEnumerable<ProductLookupModel> ViewSort(
            IEnumerable<ProductLookupModel> products, ProductSort sort)
        {
            products = sort switch
            {
                ProductSort.Newest => products.ToList(),
                ProductSort.Oldest => products.OrderByDescending(x => x.Id).ToList(),
                ProductSort.PriceAscending => products.OrderBy(x => x.Price).ToList(),
                ProductSort.PriceDescending => products.OrderByDescending(x => x.Price).ToList(),
                _ => products
            };

            return products;
        }
    }
}

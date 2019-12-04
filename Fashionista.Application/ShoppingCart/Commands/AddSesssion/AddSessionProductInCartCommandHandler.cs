namespace Fashionista.Application.ShoppingCart.Commands.AddSesssion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class AddSessionProductInCartCommandHandler :
        IRequestHandler<AddSessionProductInCartCommand, List<CartProductLookupModel>>
    {
        private readonly IShoppingCartAssistant shoppingCartAssistant;
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<ProductSize> productSizesRepository;
        private readonly IDeletableEntityRepository<ProductColor> productColorsRepository;

        public AddSessionProductInCartCommandHandler(
            IDeletableEntityRepository<Product> productsRepository,
            IShoppingCartAssistant shoppingCartAssistant,
            IDeletableEntityRepository<ProductSize> productSizesRepository,
            IDeletableEntityRepository<ProductColor> productColorsRepository)
        {
            this.shoppingCartAssistant = shoppingCartAssistant;
            this.productSizesRepository = productSizesRepository;
            this.productColorsRepository = productColorsRepository;
            this.productsRepository = productsRepository;
        }

        public async Task<List<CartProductLookupModel>> Handle(
            AddSessionProductInCartCommand request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (this.CheckIfSessionContainsProduct(request.Session, request.Id))
            {
                return null;
            }

            var requestedProduct = await this.productsRepository
                                       .AllAsNoTracking()
                                       .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                   ?? throw new NotFoundException(nameof(Product), request.Id);

            var size = await this.productSizesRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == request.SizeId, cancellationToken);

            var color = await this.productColorsRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == request.ColorId, cancellationToken);

            var newProduct = new CartProductLookupModel
            {
                ProductId = requestedProduct.Id,
                ProductName = requestedProduct.Name,
                ProductPhotos = requestedProduct.Photos,
                ProductPrice = requestedProduct.Price,
                Quantity = request.Quantity ?? 1,
                ColorName = color.Name,
                SizeName = size.Name,
            };

            request.Session.Add(newProduct);

            this.shoppingCartAssistant.Set(request.Session);

            return request.Session;
        }

        private bool CheckIfSessionContainsProduct(List<CartProductLookupModel> session, int id) =>
            session.Any(x => x.ProductId == id);
    }
}

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

    public class AddSessionProductCartCommandHandler :
        IRequestHandler<AddSessionProductInCartCommand, List<CartProductLookupModel>>
    {
        private readonly IShoppingCartAssistant shoppingCartAssistant;
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public AddSessionProductCartCommandHandler(
            IDeletableEntityRepository<Product> productsRepository,
            IShoppingCartAssistant shoppingCartAssistant)
        {
            this.shoppingCartAssistant = shoppingCartAssistant;
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

            var newProduct = new CartProductLookupModel
            {
                ProductId = requestedProduct.Id,
                ProductName = requestedProduct.Name,
                ProductPhotos = requestedProduct.Photos,
                ProductPrice = requestedProduct.Price,
                Quantity = 1,
            };

            request.Session.Add(newProduct);

            this.shoppingCartAssistant.Set(request.Session);

            return request.Session;
        }

        private bool CheckIfSessionContainsProduct(List<CartProductLookupModel> session, int id) =>
            session.Any(x => x.ProductId == id);
    }
}

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
    using Fashionista.Application.ShoppingCart.Queries.GetAllShoppingCartProducts;
    using Fashionista.Common;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public class AddSessionProductCartCommandHandler :
        IRequestHandler<AddSessionProductCartCommand, AllShoppingCartProductsViewModel>
    {
        private readonly IHttpContextAccessor accessor;
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public AddSessionProductCartCommandHandler(
            IHttpContextAccessor httpContext,
            IDeletableEntityRepository<Product> productsRepository)
        {
            this.accessor = httpContext;
            this.productsRepository = productsRepository;
        }

        public async Task<AllShoppingCartProductsViewModel> Handle(
            AddSessionProductCartCommand request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var session = this.GetObjectFromJson<List<CartProductLookupModel>>(
                              this.accessor.HttpContext.Session, GlobalConstants.ShoppingCartKey)
                          ?? new List<CartProductLookupModel>();

            if (this.CheckIfSessionContainsProduct(session, request.Id))
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

            session.Add(newProduct);

            this.SetObjectAsJson(this.accessor.HttpContext.Session, GlobalConstants.ShoppingCartKey, session);

            return new AllShoppingCartProductsViewModel
            {
                ShoppingCartProducts = session,
            };
        }

        private T GetObjectFromJson<T>(ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        private void SetObjectAsJson(ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        private bool CheckIfSessionContainsProduct(List<CartProductLookupModel> session, int id) =>
            session.Any(x => x.ProductId == id);
    }
}

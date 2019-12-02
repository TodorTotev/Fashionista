namespace Fashionista.Application.Tests.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Moq;

    public class ShoppingCartAssistantFactory
    {
        public static Mock<IShoppingCartAssistant> Create(List<Product> products)
        {
            var dummyList = new List<CartProductLookupModel>();
            products
                .Select(x => new CartProductLookupModel()
                {
                    ProductId = x.Id,
                })
                .ToList()
                .ForEach(x => dummyList.Add(x));

            var shoppingCartAssistantMock = new Mock<IShoppingCartAssistant>();
            shoppingCartAssistantMock.Setup(x => x.Get()).Returns(dummyList);

            return shoppingCartAssistantMock;
        }
    }
}
namespace Fashionista.Infrastructure
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Interfaces;
    using Fashionista.Common;
    using Microsoft.AspNetCore.Http;

    public class ShoppingCartAssistant : IShoppingCartAssistant
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ShoppingCartAssistant(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public List<CartProductLookupModel> SessionProducts
        {
            get => this.httpContextAccessor
                .HttpContext
                .Session
                .GetObjectFromJson<List<CartProductLookupModel>>(GlobalConstants.ShoppingCartKey);

            set => this.httpContextAccessor
                .HttpContext
                .Session
                .SetObjectAsJson(GlobalConstants.ShoppingCartKey, value);
        }
    }
}

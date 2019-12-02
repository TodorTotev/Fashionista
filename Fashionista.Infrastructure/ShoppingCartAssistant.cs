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

        public List<CartProductLookupModel> SessionProducts { get; set; }

        public List<CartProductLookupModel> Get()
        {
            return this.httpContextAccessor
                    .HttpContext
                    .Session
                    .GetObjectFromJson<List<CartProductLookupModel>>(GlobalConstants.ShoppingCartKey)
                ?? new List<CartProductLookupModel>();
        }

        public void Set(ICollection<CartProductLookupModel> value)
        {
            this.httpContextAccessor
                .HttpContext
                .Session
                .SetObjectAsJson(GlobalConstants.ShoppingCartKey, value);
        }
    }
}

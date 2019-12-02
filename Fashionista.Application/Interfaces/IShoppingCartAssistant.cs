namespace Fashionista.Application.Interfaces
{
    using System.Collections.Generic;
    using Fashionista.Application.Common.Models;

    public interface IShoppingCartAssistant
    {
        List<CartProductLookupModel> SessionProducts { get; set; }

        List<CartProductLookupModel> Get();

        void Set(ICollection<CartProductLookupModel> value);
    }
}

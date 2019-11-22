namespace Fashionista.Application.ProductAttributes.Queries.GetAll
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class GetAllProductAttributesViewModel
    {
        public IEnumerable<ProductAttributesLookupModel> ProductAttributesList { get; set; }
    }
}

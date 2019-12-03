namespace Fashionista.Application.Products.Queries.GetAllProductsByCategory
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;
    using MediatR;

    public class GetAllProductsByCategoryQuery : IRequest<List<ProductLookupModel>>
    {
        public int Id { get; set; }
    }
}

namespace Fashionista.Application.ProductSizes.Queries.GetAllSizesByCategory
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;
    using MediatR;

    public class GetAllSizesByCategoryQuery : IRequest<List<ProductSizeLookupModel>>
    {
        public int Id { get; set; }
    }
}
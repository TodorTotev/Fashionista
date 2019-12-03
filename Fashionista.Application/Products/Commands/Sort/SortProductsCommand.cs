namespace Fashionista.Application.Products.Commands.Sort
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;

    public class SortProductsCommand : IRequest<List<ProductLookupModel>>
    {
        public int? BrandId { get; set; }

        public int ColorId { get; set; }

        public int SizeId { get; set; }

        public ProductSort Sort { get; set; }

        public ProductType? Gender { get; set; }

        public List<ProductLookupModel> Products { get; set; }
    }
}

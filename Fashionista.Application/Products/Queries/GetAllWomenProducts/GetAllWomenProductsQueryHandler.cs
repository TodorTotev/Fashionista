namespace Fashionista.Application.Products.Queries.GetAllWomenProducts
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;
    using X.PagedList;

    public class GetAllWomenProductsQueryHandler : IRequestHandler<GetAllWomenProductsQuery, GetAllWomenProductsViewModel>
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public GetAllWomenProductsQueryHandler(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<GetAllWomenProductsViewModel> Handle(GetAllWomenProductsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException();

            var products = await this.productsRepository
                .AllAsNoTracking()
                .Where(x => x.ProductType == ProductType.Women)
                .To<ProductLookupModel>()
                .ToListAsync(cancellationToken);

            return new GetAllWomenProductsViewModel
            {
                Products = products,
            };
        }
    }
}

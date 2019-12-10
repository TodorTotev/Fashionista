namespace Fashionista.Application.Products.Queries.GetAllMenProducts
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

    public class GetAllMenProductsQueryHandler : IRequestHandler<GetAllMenProductsQuery, GetAllMenProductsViewModel>
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public GetAllMenProductsQueryHandler(
            IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<GetAllMenProductsViewModel> Handle(
            GetAllMenProductsQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(Product));

            var products = await this.productsRepository
                .AllAsNoTracking()
                .Where(x => x.ProductType == ProductType.Men)
                .To<ProductLookupModel>()
                .ToListAsync(cancellationToken);

            return new GetAllMenProductsViewModel
            {
                Products = products,
            };
        }
    }
}

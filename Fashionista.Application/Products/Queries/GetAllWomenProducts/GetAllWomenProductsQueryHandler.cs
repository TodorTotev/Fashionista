namespace Fashionista.Application.Products.Queries.GetAllWomenProducts
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;
    using X.PagedList;

    public class GetAllWomenProductsQueryHandler : IRequestHandler<GetAllWomenProductsQuery, GetAllWomenProductsViewModel>
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IMapper mapper;

        public GetAllWomenProductsQueryHandler(IDeletableEntityRepository<Product> productsRepository, IMapper mapper)
        {
            this.productsRepository = productsRepository;
            this.mapper = mapper;
        }

        public async Task<GetAllWomenProductsViewModel> Handle(GetAllWomenProductsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException();

            var products = await this.productsRepository
                .AllAsNoTracking()
                .Where(x => x.ProductType == ProductType.Women)
                .ProjectTo<ProductLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetAllWomenProductsViewModel
            {
                Products = products,
            };
        }
    }
}

namespace Fashionista.Application.Products.Queries.GetAllProductsPaged
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAllProductsPagedQueryHandler : IRequestHandler<GetAllProductsPagedQuery, GetAllProductsPagedViewModel>
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IMapper mapper;

        public GetAllProductsPagedQueryHandler(
            IDeletableEntityRepository<Product> productsRepository,
            IMapper mapper)
        {
            this.productsRepository = productsRepository;
            this.mapper = mapper;
        }

        public async Task<GetAllProductsPagedViewModel> Handle(GetAllProductsPagedQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var currentPageEntities = new List<ProductLookupModel>();

            if (request.IsActive)
            {
                currentPageEntities = await this.productsRepository
                    .AllAsNoTracking()
                    .Where(x => x.IsHidden == false)
                    .Skip(request.PageNumber * request.PageSize)
                    .Take(request.PageSize)
                    .ProjectTo<ProductLookupModel>(this.mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
            else if (request.IsActive == false)
            {
                currentPageEntities = await this.productsRepository
                    .AllAsNoTracking()
                    .Where(x => x.IsHidden == true)
                    .Skip(request.PageNumber * request.PageSize)
                    .Take(request.PageSize)
                    .ProjectTo<ProductLookupModel>(this.mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }

            return new GetAllProductsPagedViewModel
            {
                Products = currentPageEntities,
            };
        }
    }
}

namespace Fashionista.Application.Products.Queries.GetAllProductsPaged
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAllProductsPagedQueryHandler : IRequestHandler<GetAllProductsPagedQuery, GetAllProductsPagedViewModel>
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public GetAllProductsPagedQueryHandler(
            IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<GetAllProductsPagedViewModel> Handle(
            GetAllProductsPagedQuery request, CancellationToken cancellationToken)
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
                    .To<ProductLookupModel>()
                    .ToListAsync(cancellationToken);
            }
            else if (request.IsActive == false)
            {
                currentPageEntities = await this.productsRepository
                    .AllAsNoTracking()
                    .Where(x => x.IsHidden)
                    .Skip(request.PageNumber * request.PageSize)
                    .Take(request.PageSize)
                    .To<ProductLookupModel>()
                    .ToListAsync(cancellationToken);
            }

            return new GetAllProductsPagedViewModel
            {
                Products = currentPageEntities,
            };
        }
    }
}

namespace Fashionista.Application.ProductSizes.Queries.GetAllSizesSelectList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class
        GetAllSizesSelectListQueryHandler : IRequestHandler<GetAllSizesSelectListQuery, GetAllSizesSelectListViewModel>
    {
        private readonly IDeletableEntityRepository<ProductSize> productSizesRepository;

        public GetAllSizesSelectListQueryHandler(IDeletableEntityRepository<ProductSize> productSizesRepository)
        {
            this.productSizesRepository = productSizesRepository;
        }

        public async Task<GetAllSizesSelectListViewModel> Handle(GetAllSizesSelectListQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var sizes = await this.productSizesRepository
                .AllAsNoTracking()
                .Where(x => x.MainCategoryId == request.MainCategoryId)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                })
                .ToListAsync(cancellationToken);

            if (sizes.Count == 0)
            {
                return new GetAllSizesSelectListViewModel
                {
                    AllSizes = new List<SelectListItem>(),
                };
            }

            return new GetAllSizesSelectListViewModel
            {
                AllSizes = sizes,
            };
        }
    }
}

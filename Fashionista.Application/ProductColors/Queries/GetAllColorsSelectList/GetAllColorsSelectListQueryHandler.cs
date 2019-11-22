namespace Fashionista.Application.ProductColors.Queries.GetAllColorsSelectList
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class GetAllColorsSelectListQueryHandler : IRequestHandler<GetAllColorsSelectListQuery, GetAllColorsSelectListViewModel>
    {
        private readonly IDeletableEntityRepository<ProductColor> productColorsRepository;

        public GetAllColorsSelectListQueryHandler(IDeletableEntityRepository<ProductColor> productColorsRepository)
        {
            this.productColorsRepository = productColorsRepository;
        }

        public async Task<GetAllColorsSelectListViewModel> Handle(GetAllColorsSelectListQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var colors = await this.productColorsRepository
                .AllAsNoTracking()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                })
                .ToListAsync(cancellationToken);

            return new GetAllColorsSelectListViewModel
            {
                AllColors = colors,
            };
        }
    }
}

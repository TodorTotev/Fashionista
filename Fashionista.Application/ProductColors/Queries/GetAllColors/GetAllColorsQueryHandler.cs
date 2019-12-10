namespace Fashionista.Application.ProductColors.Queries.GetAllColors
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAllColorsQueryHandler : IRequestHandler<GetAllColorsQuery, List<ProductColorLookupModel>>
    {
        private readonly IDeletableEntityRepository<ProductColor> productColorsRepository;

        public GetAllColorsQueryHandler(IDeletableEntityRepository<ProductColor> productColorsRepository)
        {
            this.productColorsRepository = productColorsRepository;
        }

        public async Task<List<ProductColorLookupModel>> Handle(GetAllColorsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            return await this.productColorsRepository
                .All()
                .To<ProductColorLookupModel>()
                .ToListAsync(cancellationToken);
        }
    }
}

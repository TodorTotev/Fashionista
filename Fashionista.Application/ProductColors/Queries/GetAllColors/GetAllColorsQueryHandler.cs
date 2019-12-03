namespace Fashionista.Application.ProductColors.Queries.GetAllColors
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAllColorsQueryHandler : IRequestHandler<GetAllColorsQuery, List<ProductColorLookupModel>>
    {
        private readonly IDeletableEntityRepository<ProductColor> productColorsRepository;
        private readonly IMapper mapper;

        public GetAllColorsQueryHandler(IDeletableEntityRepository<ProductColor> productColorsRepository, IMapper mapper)
        {
            this.productColorsRepository = productColorsRepository;
            this.mapper = mapper;
        }

        public async Task<List<ProductColorLookupModel>> Handle(GetAllColorsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            return await this.productColorsRepository
                .All()
                .ProjectTo<ProductColorLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}

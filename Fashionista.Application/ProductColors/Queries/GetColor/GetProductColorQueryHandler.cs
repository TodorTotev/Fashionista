using System.Linq;

namespace Fashionista.Application.ProductColors.Queries.GetColor
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetProductColorQueryHandler : IRequestHandler<GetProductColorQuery, ProductColorLookupModel>
    {
        private readonly IDeletableEntityRepository<ProductColor> colorsRepository;
        private readonly IMapper mapper;

        public GetProductColorQueryHandler(
            IDeletableEntityRepository<ProductColor> colorsRepository,
            IMapper mapper)
        {
            this.colorsRepository = colorsRepository;
            this.mapper = mapper;
        }

        public async Task<ProductColorLookupModel> Handle(GetProductColorQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckAssistant.CheckIfProductColorExists(request.Id, this.colorsRepository))
            {
                throw new NotFoundException(nameof(ProductColor), "Color doesnt exist!");
            }

            return await this.colorsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == request.Id)
                .ProjectTo<ProductColorLookupModel>(this.mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}

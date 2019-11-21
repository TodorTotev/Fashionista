namespace Fashionista.Application.ProductSizes.Queries.GetSize
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

    public class GetProductSizeQueryHandler : IRequestHandler<GetProductSizeQuery, ProductSizeLookupModel>
    {
        private readonly IDeletableEntityRepository<ProductSize> productSizesRepository;
        private readonly IMapper mapper;

        public GetProductSizeQueryHandler(
            IDeletableEntityRepository<ProductSize> productSizesRepository,
            IMapper mapper)
        {
            this.productSizesRepository = productSizesRepository;
            this.mapper = mapper;
        }

        public async Task<ProductSizeLookupModel> Handle(GetProductSizeQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckAssistant.CheckIfProductSizeExists(request.Id, this.productSizesRepository))
            {
                throw new NotFoundException(nameof(ProductSize), "Size not found");
            }

            return await this.productSizesRepository
                .AllAsNoTracking()
                .ProjectTo<ProductSizeLookupModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}

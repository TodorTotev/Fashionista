namespace Fashionista.Application.ProductSizes.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateProductSizeCommandHandler : IRequestHandler<CreateProductSizeCommand, int>
    {
        private readonly IDeletableEntityRepository<ProductSize> productSizeRepository;
        private readonly IMapper mapper;

        public CreateProductSizeCommandHandler(
            IDeletableEntityRepository<ProductSize> productSizeRepository,
            IMapper mapper)
        {
            this.productSizeRepository = productSizeRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateProductSizeCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var productSize = this.mapper.Map<ProductSize>(request);
            await this.productSizeRepository.AddAsync(productSize);
            await this.productSizeRepository.SaveChangesAsync(cancellationToken);

            return productSize.Id;
        }
    }
}
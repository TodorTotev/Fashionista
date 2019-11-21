namespace Fashionista.Application.ProductColors.Commands.CreateColor
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, int>
    {
        private readonly IDeletableEntityRepository<ProductColor> productColorsRepository;
        private readonly IMapper mapper;

        public CreateColorCommandHandler(
            IDeletableEntityRepository<ProductColor> productColorsRepository,
            IMapper mapper)
        {
            this.productColorsRepository = productColorsRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateColorCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var productColor = this.mapper.Map<ProductColor>(request);
            await this.productColorsRepository.AddAsync(productColor);
            await this.productColorsRepository.SaveChangesAsync(cancellationToken);

            return productColor.Id;
        }
    }
}

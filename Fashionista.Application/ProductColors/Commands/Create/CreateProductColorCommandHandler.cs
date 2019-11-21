namespace Fashionista.Application.ProductColors.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateProductColorCommandHandler : IRequestHandler<CreateProductColorCommand, int>
    {
        private readonly IDeletableEntityRepository<ProductColor> productColorsRepository;
        private readonly IMapper mapper;

        public CreateProductColorCommandHandler(
            IDeletableEntityRepository<ProductColor> productColorsRepository,
            IMapper mapper)
        {
            this.productColorsRepository = productColorsRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateProductColorCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (await CommonCheckAssistant.CheckIfProductColorWithSameNameExists(
                request.Name,
                this.productColorsRepository))
            {
                throw new EntityAlreadyExistsException(nameof(ProductColor), request.Name);
            }

            var productColor = this.mapper.Map<ProductColor>(request);
            await this.productColorsRepository.AddAsync(productColor);
            await this.productColorsRepository.SaveChangesAsync(cancellationToken);

            return productColor.Id;
        }
    }
}

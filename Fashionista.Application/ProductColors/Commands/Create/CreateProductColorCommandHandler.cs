namespace Fashionista.Application.ProductColors.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateProductColorCommandHandler : IRequestHandler<CreateProductColorCommand, int>
    {
        private readonly IDeletableEntityRepository<ProductColor> productColorsRepository;

        public CreateProductColorCommandHandler(
            IDeletableEntityRepository<ProductColor> productColorsRepository)
        {
            this.productColorsRepository = productColorsRepository;
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

            var productColor = request.To<ProductColor>();
            await this.productColorsRepository.AddAsync(productColor);
            await this.productColorsRepository.SaveChangesAsync(cancellationToken);

            return productColor.Id;
        }
    }
}

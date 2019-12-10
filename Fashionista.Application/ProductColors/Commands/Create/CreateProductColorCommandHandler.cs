namespace Fashionista.Application.ProductColors.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

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

            if (await this.CheckIfProductColorWithSameNameExists(
                request.Name))
            {
                throw new EntityAlreadyExistsException(nameof(ProductColor), request.Name);
            }

            var productColor = request.To<ProductColor>();
            await this.productColorsRepository.AddAsync(productColor);
            await this.productColorsRepository.SaveChangesAsync(cancellationToken);

            return productColor.Id;
        }

        private async Task<bool> CheckIfProductColorWithSameNameExists(
            string name) =>
         await this.productColorsRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
    }
}

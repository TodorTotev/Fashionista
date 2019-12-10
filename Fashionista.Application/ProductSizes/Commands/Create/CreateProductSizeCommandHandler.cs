namespace Fashionista.Application.ProductSizes.Commands.Create
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

    public class CreateProductSizeCommandHandler : IRequestHandler<CreateProductSizeCommand, int>
    {
        private readonly IDeletableEntityRepository<ProductSize> productSizeRepository;

        public CreateProductSizeCommandHandler(
            IDeletableEntityRepository<ProductSize> productSizeRepository)
        {
            this.productSizeRepository = productSizeRepository;
        }

        public async Task<int> Handle(CreateProductSizeCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (await this.CheckIfProductSizeWithSameNameExists(
                request.Name,
                request.MainCategoryId))
            {
                throw new EntityAlreadyExistsException(nameof(ProductSize), request.Name);
            }

            var productSize = request.To<ProductSize>();
            await this.productSizeRepository.AddAsync(productSize);
            await this.productSizeRepository.SaveChangesAsync(cancellationToken);

            return productSize.Id;
        }

        private async Task<bool> CheckIfProductSizeWithSameNameExists(
            string name,
            int id) =>
            await this.productSizeRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name && x.MainCategoryId == id);
    }
}

namespace Fashionista.Application.ProductAttributes.Commands.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class CreateProductAttributeCommandHandler : IRequestHandler<CreateProductAttributeCommand, int>
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public CreateProductAttributeCommandHandler(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<int> Handle(CreateProductAttributeCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var product = await this.productsRepository
                .All()
                .SingleOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

            if (this.CheckIfProductContainsAttribute(product, request.ProductColorId, request.ProductSizeId))
            {
                throw new ProductContainsAttributeException();
            }

            product.ProductAttributes.Add(new ProductAttributes
            {
                ProductSizeId = request.ProductSizeId,
                ProductColorId = request.ProductColorId,
            });

            this.productsRepository.Update(product);
            await this.productsRepository.SaveChangesAsync(cancellationToken);

            return product.Id;
        }

        private bool CheckIfProductContainsAttribute(Product product, int colorId, int sizeId) =>
            product.ProductAttributes.Any(x => x.ProductColorId == colorId && x.ProductSizeId == sizeId);
    }
}

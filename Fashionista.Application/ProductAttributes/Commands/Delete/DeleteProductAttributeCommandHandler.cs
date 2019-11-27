namespace Fashionista.Application.ProductAttributes.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class DeleteProductAttributeCommandHandler : IRequestHandler<DeleteProductAttributeCommand, int>
    {
        private readonly IDeletableEntityRepository<ProductAttributes> productAttributesRepository;

        public DeleteProductAttributeCommandHandler(IDeletableEntityRepository<ProductAttributes> productAttributesRepository)
        {
            this.productAttributesRepository = productAttributesRepository;
        }

        public async Task<int> Handle(DeleteProductAttributeCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var requestedEntity = await this.productAttributesRepository
                .GetByIdWithDeletedAsync(request.Id)
                                  ?? throw new NotFoundException(nameof(ProductAttributes), request.Id);

            this.productAttributesRepository.Delete(requestedEntity);
            await this.productAttributesRepository.SaveChangesAsync(cancellationToken);

            return requestedEntity.Id;
        }
    }
}

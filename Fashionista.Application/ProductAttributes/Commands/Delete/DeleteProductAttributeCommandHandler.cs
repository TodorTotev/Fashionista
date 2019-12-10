namespace Fashionista.Application.ProductAttributes.Commands.Delete
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

    using static Fashionista.Common.GlobalConstants;

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
                                      .AllWithDeleted()
                                      .Where(x => x.Id == request.Id)
                                      .SingleOrDefaultAsync(cancellationToken)
                                  ?? throw new NotFoundException(nameof(ProductAttributes), request.Id);

            if (requestedEntity.IsDeleted)
            {
                throw new FailedDeletionException(nameof(ProductAttributes), request.Id, EntityAlreadyDeletedMessage);
            }

            this.productAttributesRepository.Delete(requestedEntity);
            await this.productAttributesRepository.SaveChangesAsync(cancellationToken);

            return requestedEntity.Id;
        }
    }
}

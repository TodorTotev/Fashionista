namespace Fashionista.Application.ProductAttributes.Commands.Edit
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class EditProductAttributeCommandHandler : IRequestHandler<EditProductAttributeCommand, int>
    {
        private readonly IDeletableEntityRepository<ProductAttributes> productAttributesRepository;

        public EditProductAttributeCommandHandler(
            IDeletableEntityRepository<ProductAttributes> productAttributesRepository)
        {
            this.productAttributesRepository = productAttributesRepository;
        }

        public async Task<int> Handle(EditProductAttributeCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(ProductAttributes));

            var requestedEntity = await this.productAttributesRepository
                                      .All()
                                      .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                  ?? throw new NotFoundException(nameof(ProductAttributes), request.ProductId);

            requestedEntity.Quantity = request.Quantity;
            await this.productAttributesRepository.SaveChangesAsync(cancellationToken);

            return requestedEntity.Id;
        }
    }
}

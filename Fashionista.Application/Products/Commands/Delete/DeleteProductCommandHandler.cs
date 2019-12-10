namespace Fashionista.Application.Products.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using static Fashionista.Common.GlobalConstants;

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public DeleteProductCommandHandler(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var requestedEntity = await this.productsRepository
                                      .AllWithDeleted()
                                      .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                  ?? throw new NotFoundException(nameof(Product), request.Id);

            if (requestedEntity.IsDeleted)
            {
                throw new FailedDeletionException(nameof(Product), request.Id, DeletedNotificationMsg);
            }

            this.productsRepository.Delete(requestedEntity);
            await this.productsRepository.SaveChangesAsync(cancellationToken);

            return requestedEntity.Id;
        }
    }
}
namespace Fashionista.Application.Brands.Commands.Delete
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

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, int>
    {
        private readonly IDeletableEntityRepository<Brand> brandRepository;

        public DeleteBrandCommandHandler(IDeletableEntityRepository<Brand> brandRepository)
        {
            this.brandRepository = brandRepository;
        }

        public async Task<int> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var requestedEntity = await this.brandRepository
                                      .AllWithDeleted()
                                      .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                  ?? throw new NotFoundException(nameof(MainCategory), request.Id);

            if (requestedEntity.IsDeleted)
            {
                throw new FailedDeletionException(nameof(Brand), request.Id, EntityAlreadyDeletedMessage);
            }

            this.brandRepository.Delete(requestedEntity);
            await this.brandRepository.SaveChangesAsync(cancellationToken);

            return requestedEntity.Id;
        }
    }
}

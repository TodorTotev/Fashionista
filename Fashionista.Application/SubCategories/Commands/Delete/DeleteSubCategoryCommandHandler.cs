namespace Fashionista.Application.SubCategories.Commands.Delete
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

    public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommand, int>
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public DeleteSubCategoryCommandHandler(IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        public async Task<int> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var requestedEntity = await this.subCategoryRepository
                                      .AllWithDeleted()
                                      .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                  ?? throw new NotFoundException(nameof(SubCategory), request.Id);

            if (requestedEntity.IsDeleted)
            {
                throw new FailedDeletionException(nameof(SubCategory), request.Id, EntityAlreadyDeletedMessage);
            }

            this.subCategoryRepository.Delete(requestedEntity);
            await this.subCategoryRepository.SaveChangesAsync(cancellationToken);

            return requestedEntity.Id;
        }
    }
}

namespace Fashionista.Application.SubCategories.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

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
                                      .GetByIdWithDeletedAsync(request.Id, cancellationToken)
                                  ?? throw new NotFoundException(nameof(SubCategory), request.Id);

            this.subCategoryRepository.Delete(requestedEntity);
            await this.subCategoryRepository.SaveChangesAsync(cancellationToken);

            return requestedEntity.Id;
        }
    }
}
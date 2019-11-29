namespace Fashionista.Application.SubCategories.Commands.Edit
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class EditSubCategoryCommandHandler : IRequestHandler<EditSubCategoryCommand, int>
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public EditSubCategoryCommandHandler(IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        public async Task<int> Handle(EditSubCategoryCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (await CommonCheckAssistant.CheckIfSubCategoryWithSameNameExists(request.Name,
                this.subCategoryRepository))
            {
                throw new EntityAlreadyExistsException(nameof(SubCategory), request.Name);
            }

            var requestedEntity = await this.subCategoryRepository
                                      .All()
                                      .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                  ?? throw new NotFoundException(nameof(SubCategory), request.Id);

            requestedEntity.Name = request.Name;
            requestedEntity.Description = request.Description;
            await this.subCategoryRepository.SaveChangesAsync(cancellationToken);

            return requestedEntity.Id;
        }
    }
}
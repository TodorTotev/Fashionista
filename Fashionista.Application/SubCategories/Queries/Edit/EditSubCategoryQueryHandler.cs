namespace Fashionista.Application.SubCategories.Queries.Edit
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.SubCategories.Commands.Edit;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class EditSubCategoryQueryHandler : IRequestHandler<EditSubCategoryQuery, EditSubCategoryCommand>
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public EditSubCategoryQueryHandler(
            IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        public async Task<EditSubCategoryCommand> Handle(EditSubCategoryQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var requestedEntity = await this.subCategoryRepository
                                      .AllAsNoTracking()
                                      .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                  ?? throw new NotFoundException(nameof(SubCategory), request.Id);

            var command = requestedEntity.To<EditSubCategoryCommand>();
            return command;
        }
    }
}

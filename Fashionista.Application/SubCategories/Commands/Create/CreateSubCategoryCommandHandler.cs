namespace Fashionista.Application.SubCategories.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand, int>
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public CreateSubCategoryCommandHandler(
            IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        public async Task<int> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (await CommonCheckAssistant.CheckIfSubCategoryWithSameNameExists(request.Name, this.subCategoryRepository))
            {
                throw new EntityAlreadyExistsException(nameof(SubCategory), request.Name);
            }

            var category = request.To<SubCategory>();

            await this.subCategoryRepository.AddAsync(category);
            await this.subCategoryRepository.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}

namespace Fashionista.Application.MainCategories.Commands.Create
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

    public class CreateMainCategoryCommandHandler : IRequestHandler<CreateMainCategoryCommand, int>
    {
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;

        public CreateMainCategoryCommandHandler(
            IDeletableEntityRepository<MainCategory> mainCategoryRepository)
        {
            this.mainCategoryRepository = mainCategoryRepository;
        }

        public async Task<int> Handle(CreateMainCategoryCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (await CommonCheckAssistant.CheckIfMainCategoryWithSameNameExists(request.Name, this.mainCategoryRepository))
            {
                throw new EntityAlreadyExistsException(nameof(MainCategory), request.Name);
            }

            var category = request.To<MainCategory>();

            await this.mainCategoryRepository.AddAsync(category);
            await this.mainCategoryRepository.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}

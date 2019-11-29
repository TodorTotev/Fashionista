namespace Fashionista.Application.MainCategories.Commands.Edit
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

    public class EditMainCategoryCommandHandler : IRequestHandler<EditMainCategoryCommand, int>
    {
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;

        public EditMainCategoryCommandHandler(
            IDeletableEntityRepository<MainCategory> mainCategoryRepository)
        {
            this.mainCategoryRepository = mainCategoryRepository;
        }

        public async Task<int> Handle(EditMainCategoryCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (await CommonCheckAssistant.CheckIfMainCategoryWithSameNameExists(request.Name, this.mainCategoryRepository))
            {
                throw new EntityAlreadyExistsException(nameof(MainCategory), request.Name);
            }

            var requestedEntity = await this.mainCategoryRepository
                .All()
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                  ?? throw new NotFoundException(nameof(MainCategory), request.Id);

            requestedEntity.Name = request.Name;
            this.mainCategoryRepository.Update(requestedEntity);
            await this.mainCategoryRepository.SaveChangesAsync(cancellationToken);

            return requestedEntity.Id;
        }
    }
}

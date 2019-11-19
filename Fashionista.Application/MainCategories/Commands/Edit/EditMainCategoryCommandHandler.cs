using Microsoft.EntityFrameworkCore;

namespace Fashionista.Application.MainCategories.Commands.Edit
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class EditMainCategoryCommandHandler : IRequestHandler<EditMainCategoryCommand, int>
    {
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;
        private readonly IMapper mapper;

        public EditMainCategoryCommandHandler(
            IDeletableEntityRepository<MainCategory> mainCategoryRepository,
            IMapper mapper)
        {
            this.mainCategoryRepository = mainCategoryRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(EditMainCategoryCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (await CommonCheckAssistant.CheckIfMainCategoryWithSameNameExists(request.Name, this.mainCategoryRepository))
            {
                throw new EntityAlreadyExistsException(nameof(MainCategory), request.Name);
            }

            var requestedEntity = await this.mainCategoryRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            this.mapper.Map(request, requestedEntity);
            await this.mainCategoryRepository.SaveChangesAsync();

            return requestedEntity.Id;
        }
    }
}

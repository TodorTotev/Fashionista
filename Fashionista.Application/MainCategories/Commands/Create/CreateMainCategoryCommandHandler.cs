namespace Fashionista.Application.MainCategories.Commands.Create
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

    public class CreateMainCategoryCommandHandler : IRequestHandler<CreateMainCategoryCommand, int>
    {
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;
        private readonly IMapper mapper;

        public CreateMainCategoryCommandHandler(
            IDeletableEntityRepository<MainCategory> mainCategoryRepository,
            IMapper mapper)
        {
            this.mapper = mapper;
            this.mainCategoryRepository = mainCategoryRepository;
        }

        public async Task<int> Handle(CreateMainCategoryCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (await CommonCheckAssistant.CheckIfMainCategoryWithSameNameExists(request.Name, this.mainCategoryRepository))
            {
                throw new EntityAlreadyExistsException(nameof(MainCategory), request.Name);
            }

            var category = this.mapper.Map<MainCategory>(request);

            await this.mainCategoryRepository.AddAsync(category);
            await this.mainCategoryRepository.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}

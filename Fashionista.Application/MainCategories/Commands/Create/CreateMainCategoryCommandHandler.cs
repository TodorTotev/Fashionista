namespace Fashionista.Application.MainCategories.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
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

        public async Task<int> Handle(CreateMainCategoryCommand request, CancellationToken token)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var category = this.mapper.Map<MainCategory>(request);

            await this.mainCategoryRepository.AddAsync(category);
            await this.mainCategoryRepository.SaveChangesAsync();

            return category.Id;
        }
    }
}

namespace Fashionista.Application.SubCategories.Commands.Create
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

    public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand, int>
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;
        private readonly IMapper mapper;

        public CreateSubCategoryCommandHandler(
            IDeletableEntityRepository<SubCategory> subCategoryRepository,
            IMapper mapper)
        {
            this.subCategoryRepository = subCategoryRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (await CommonCheckAssistant.CheckIfSubCategoryWithSameNameExists(nameof(SubCategory), this.subCategoryRepository))
            {
                throw new EntityAlreadyExistsException(nameof(SubCategory), request.Name);
            }

            var category = this.mapper.Map<SubCategory>(request);

            await this.subCategoryRepository.AddAsync(category);
            await this.subCategoryRepository.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}

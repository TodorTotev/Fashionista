using System;
using Fashionista.Application.Exceptions;

namespace Fashionista.Application.SubCategories.Queries.Edit
{
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.SubCategories.Commands.Edit;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class EditSubCategoryQueryHandler : IRequestHandler<EditSubCategoryQuery, EditSubCategoryCommand>
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;
        private readonly IMapper mapper;

        public EditSubCategoryQueryHandler(
            IDeletableEntityRepository<SubCategory> subCategoryRepository,
            IMapper mapper)
        {
            this.subCategoryRepository = subCategoryRepository;
            this.mapper = mapper;
        }

        public async Task<EditSubCategoryCommand> Handle(EditSubCategoryQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var requestedEntity = await this.subCategoryRepository
                                      .GetByIdWithDeletedAsync(request.Id)
                                  ?? throw new NotFoundException(nameof(SubCategory), request.Id);

            var command = this.mapper.Map<EditSubCategoryCommand>(requestedEntity);
            return command;
        }
    }
}

namespace Fashionista.Application.MainCategories.Queries.Edit
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.MainCategories.Commands.Edit;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class EditMainCategoryQueryHandler : IRequestHandler<EditMainCategoryQuery, EditMainCategoryCommand>
    {
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;
        private readonly IMapper mapper;

        public EditMainCategoryQueryHandler(
            IDeletableEntityRepository<MainCategory> mainCategoryRepository,
            IMapper mapper)
        {
            this.mainCategoryRepository = mainCategoryRepository;
            this.mapper = mapper;
        }

        public async Task<EditMainCategoryCommand> Handle(EditMainCategoryQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var requestedEntity = await this.mainCategoryRepository
                                      .AllAsNoTracking()
                                      .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                  ?? throw new NotFoundException(nameof(MainCategory), request.Id);

            var command = this.mapper.Map<EditMainCategoryCommand>(requestedEntity);
            return command;
        }
    }
}

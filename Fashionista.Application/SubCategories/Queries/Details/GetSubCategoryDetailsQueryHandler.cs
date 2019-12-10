namespace Fashionista.Application.SubCategories.Queries.Details
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetSubCategoryDetailsQueryHandler : IRequestHandler<GetSubCategoryDetailsQuery, SubCategoryLookupModel>
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoriesRepository;

        public GetSubCategoryDetailsQueryHandler(
            IDeletableEntityRepository<SubCategory> subCategoriesRepository)
        {
            this.subCategoriesRepository = subCategoriesRepository;
        }

        public async Task<SubCategoryLookupModel> Handle(GetSubCategoryDetailsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var requestedEntity = await this.subCategoriesRepository
                                      .AllAsNoTracking()
                                      .Where(x => x.Id == request.Id)
                                      .To<SubCategoryLookupModel>()
                                      .SingleOrDefaultAsync(cancellationToken)
                                  ?? throw new NotFoundException(nameof(SubCategory), request.Id);

            return requestedEntity;
        }
    }
}

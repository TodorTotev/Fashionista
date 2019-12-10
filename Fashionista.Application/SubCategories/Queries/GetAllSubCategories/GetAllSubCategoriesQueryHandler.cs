namespace Fashionista.Application.SubCategories.Queries.GetAllSubCategories
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAllSubCategoriesQueryHandler :
        IRequestHandler<GetAllSubCategoriesQuery, GetAllSubCategoriesViewModel>
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public GetAllSubCategoriesQueryHandler(
            IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        public async Task<GetAllSubCategoriesViewModel> Handle(GetAllSubCategoriesQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var currentEntities = await this.subCategoryRepository
                .AllAsNoTracking()
                .To<SubCategoryLookupModel>()
                .ToListAsync(cancellationToken);

            return new GetAllSubCategoriesViewModel
            {
                SubCategories = currentEntities,
            };
        }
    }
}

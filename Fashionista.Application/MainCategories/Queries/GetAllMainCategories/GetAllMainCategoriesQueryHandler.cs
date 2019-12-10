namespace Fashionista.Application.MainCategories.Queries.GetAllMainCategories
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

    public class GetAllMainCategoriesQueryHandler :
        IRequestHandler<GetAllMainCategoriesQuery, GetAllMainCategoriesViewModel>
    {
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;

        public GetAllMainCategoriesQueryHandler(
            IDeletableEntityRepository<MainCategory> mainCategoryRepository)
        {
            this.mainCategoryRepository = mainCategoryRepository;
        }

        public async Task<GetAllMainCategoriesViewModel> Handle(
            GetAllMainCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var currentEntities = await this.mainCategoryRepository
                .AllAsNoTracking()
                .To<MainCategoryLookupModel>()
                .ToListAsync(cancellationToken);

            return new GetAllMainCategoriesViewModel
            {
                MainCategories = currentEntities,
            };
        }
    }
}

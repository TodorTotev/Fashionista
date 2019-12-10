namespace Fashionista.Application.MainCategories.Queries.GetAllMainCategoriesNavigation
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

    public class GetAllMainCategoriesNavigationQueryHandler :
        IRequestHandler<GetAllMainCategoriesNavigationQuery, CategoriesNavigationViewModel>
    {
        private readonly IDeletableEntityRepository<MainCategory> mainCategoriesRepository;

        public GetAllMainCategoriesNavigationQueryHandler(
            IDeletableEntityRepository<MainCategory> mainCategoriesRepository)
        {
            this.mainCategoriesRepository = mainCategoriesRepository;
        }

        public async Task<CategoriesNavigationViewModel> Handle(
            GetAllMainCategoriesNavigationQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var currentEntities = await this.mainCategoriesRepository
                .AllAsNoTracking()
                .To<MainCategoryLookupModel>()
                .ToListAsync(cancellationToken);

            return new CategoriesNavigationViewModel
            {
                Categories = currentEntities,
            };
        }
    }
}

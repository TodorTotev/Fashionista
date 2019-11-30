namespace Fashionista.Application.MainCategories.Queries.GetAllMainCategoriesNavigation
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAllMainCategoriesNavigationQueryHandler :
        IRequestHandler<GetAllMainCategoriesNavigationQuery, CategoriesNavigationViewModel>
    {
        private readonly IDeletableEntityRepository<MainCategory> mainCategoriesRepository;
        private readonly IMapper mapper;

        public GetAllMainCategoriesNavigationQueryHandler(
            IDeletableEntityRepository<MainCategory> mainCategoriesRepository,
            IMapper mapper)
        {
            this.mainCategoriesRepository = mainCategoriesRepository;
            this.mapper = mapper;
        }

        public async Task<CategoriesNavigationViewModel> Handle(
            GetAllMainCategoriesNavigationQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            request = request ?? throw new ArgumentNullException(nameof(request));

            var currentEntities = await this.mainCategoriesRepository
                .AllAsNoTracking()
                .ProjectTo<MainCategoryLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CategoriesNavigationViewModel
            {
                Categories = currentEntities,
            };
        }
    }
}

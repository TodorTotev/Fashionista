namespace Fashionista.Application.MainCategories.Queries.GetAllMainCategories
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

    public class GetAllMainCategoriesQueryHandler :
        IRequestHandler<GetAllMainCategoriesQuery, GetAllMainCategoriesViewModel>
    {
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;
        private readonly IMapper mapper;

        public GetAllMainCategoriesQueryHandler(
            IDeletableEntityRepository<MainCategory> mainCategoryRepository,
            IMapper mapper)
        {
            this.mainCategoryRepository = mainCategoryRepository;
            this.mapper = mapper;
        }

        public async Task<GetAllMainCategoriesViewModel> Handle(
            GetAllMainCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var currentEntities = await this.mainCategoryRepository
                .AllAsNoTracking()
                .ProjectTo<MainCategoryLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetAllMainCategoriesViewModel
            {
                MainCategories = currentEntities,
            };
        }
    }
}

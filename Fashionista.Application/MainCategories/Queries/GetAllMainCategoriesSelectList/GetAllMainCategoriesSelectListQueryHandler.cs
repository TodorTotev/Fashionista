namespace Fashionista.Application.MainCategories.Queries.GetAllMainCategoriesSelectList
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAllMainCategoriesSelectListQueryHandler :
        IRequestHandler<GetAllMainCategoriesSelectListQuery, IEnumerable<MainCategoryLookupModel>>
    {
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;
        private readonly IMapper mapper;

        public GetAllMainCategoriesSelectListQueryHandler(
            IDeletableEntityRepository<MainCategory> mainCategoryRepository,
            IMapper mapper)
        {
            this.mainCategoryRepository = mainCategoryRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<MainCategoryLookupModel>> Handle(
            GetAllMainCategoriesSelectListQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            return await this.mainCategoryRepository
                .AllAsNoTracking()
                .ProjectTo<MainCategoryLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
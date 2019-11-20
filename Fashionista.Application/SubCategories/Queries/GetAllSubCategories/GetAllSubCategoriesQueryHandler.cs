namespace Fashionista.Application.SubCategories.Queries.GetAllSubCategories
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

    public class GetAllSubCategoriesQueryHandler :
        IRequestHandler<GetAllSubCategoriesQuery, GetAllSubCategoriesViewModel>
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;
        private readonly IMapper mapper;

        public GetAllSubCategoriesQueryHandler(
            IDeletableEntityRepository<SubCategory> subCategoryRepository,
            IMapper mapper)
        {
            this.subCategoryRepository = subCategoryRepository;
            this.mapper = mapper;
        }

        public async Task<GetAllSubCategoriesViewModel> Handle(GetAllSubCategoriesQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var currentEntities = await this.subCategoryRepository
                .AllAsNoTracking()
                .ProjectTo<SubCategoryLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetAllSubCategoriesViewModel
            {
                SubCategories = currentEntities,
            };
        }
    }
}

using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fashionista.Application.SubCategories.Queries.GetAllSubCategoriesSelectList
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

    public class GetAllSubCategoriesSelectListQueryHandler :
        IRequestHandler<GetAllSubCategoriesSelectListQuery, GetAllSubCategoriesSelectListViewModel>
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;
        private readonly IMapper mapper;

        public GetAllSubCategoriesSelectListQueryHandler(
            IDeletableEntityRepository<SubCategory> subCategoryRepository,
            IMapper mapper)
        {
            this.subCategoryRepository = subCategoryRepository;
            this.mapper = mapper;
        }

        public async Task<GetAllSubCategoriesSelectListViewModel> Handle(
            GetAllSubCategoriesSelectListQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var categories = await this.subCategoryRepository
                .AllAsNoTracking()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                })
                .ToListAsync(cancellationToken);

            return new GetAllSubCategoriesSelectListViewModel
            {
                SubCategories = categories,
            };
        }
    }
}

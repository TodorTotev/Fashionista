using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fashionista.Application.SubCategories.Queries.GetAllSubCategoriesSelectList
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAllSubCategoriesSelectListQueryHandler :
        IRequestHandler<GetAllSubCategoriesSelectListQuery, GetAllSubCategoriesSelectListViewModel>
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public GetAllSubCategoriesSelectListQueryHandler(
            IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
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

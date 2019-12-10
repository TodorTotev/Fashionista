namespace Fashionista.Application.MainCategories.Queries.GetAllMainCategoriesSelectList
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class GetAllMainCategoriesSelectListQueryHandler :
        IRequestHandler<GetAllMainCategoriesSelectListQuery, GetAllMainCategoriesSelectListViewModel>
    {
        private readonly IDeletableEntityRepository<MainCategory> mainCategoryRepository;

        public GetAllMainCategoriesSelectListQueryHandler(
            IDeletableEntityRepository<MainCategory> mainCategoryRepository)
        {
            this.mainCategoryRepository = mainCategoryRepository;
        }

        public async Task<GetAllMainCategoriesSelectListViewModel> Handle(
            GetAllMainCategoriesSelectListQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var categories = await this.mainCategoryRepository
                .AllAsNoTracking()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                })
                .ToListAsync(cancellationToken);

            return new GetAllMainCategoriesSelectListViewModel
            {
                MainCategories = categories,
            };
        }
    }
}

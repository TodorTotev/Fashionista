namespace Fashionista.Application.Brands.Queries.GetAllBrandsSelectList
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
    using X.PagedList;

    public class GetAllBrandsSelectListQueryHandler :
        IRequestHandler<GetAllBrandsSelectListQuery, GetAllBrandsSelectListViewModel>
    {
        private readonly IDeletableEntityRepository<Brand> brandsRepository;

        public GetAllBrandsSelectListQueryHandler(
            IDeletableEntityRepository<Brand> brandsRepository)
        {
            this.brandsRepository = brandsRepository;
        }

        public async Task<GetAllBrandsSelectListViewModel> Handle(GetAllBrandsSelectListQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var brands = await this.brandsRepository
                .AllAsNoTracking()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                })
                .ToListAsync(cancellationToken);

            return new GetAllBrandsSelectListViewModel
            {
                Brands = brands,
            };
        }
    }
}

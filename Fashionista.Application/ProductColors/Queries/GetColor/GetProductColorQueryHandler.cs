namespace Fashionista.Application.ProductColors.Queries.GetColor
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetProductColorQueryHandler : IRequestHandler<GetProductColorQuery, ProductColorLookupModel>
    {
        private readonly IDeletableEntityRepository<ProductColor> colorsRepository;

        public GetProductColorQueryHandler(
            IDeletableEntityRepository<ProductColor> colorsRepository)
        {
            this.colorsRepository = colorsRepository;
        }

        public async Task<ProductColorLookupModel> Handle(GetProductColorQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckAssistant.CheckIfProductColorExists(request.Id, this.colorsRepository))
            {
                throw new NotFoundException(nameof(ProductColor), "Color doesnt exist!");
            }

            return await this.colorsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == request.Id)
                .To<ProductColorLookupModel>()
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}

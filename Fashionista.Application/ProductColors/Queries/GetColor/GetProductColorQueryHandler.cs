namespace Fashionista.Application.ProductColors.Queries.GetColor
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetProductColorQueryHandler : IRequestHandler<GetProductColorQuery, ProductColor>
    {
        private readonly IDeletableEntityRepository<ProductColor> colorsRepository;

        public GetProductColorQueryHandler(IDeletableEntityRepository<ProductColor> colorsRepository)
        {
            this.colorsRepository = colorsRepository;
        }

        public async Task<ProductColor> Handle(GetProductColorQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckAssistant.CheckIfProductColorExistsByName(request.Id, this.colorsRepository))
            {
                throw new NotFoundException(nameof(ProductColor), "Color doesnt exist!");
            }

            return await this.colorsRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}

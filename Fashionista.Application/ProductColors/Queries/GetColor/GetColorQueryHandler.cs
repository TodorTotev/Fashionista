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

    public class GetColorQueryHandler : IRequestHandler<GetColorQuery, ProductColor>
    {
        private readonly IDeletableEntityRepository<ProductColor> colorsRepository;

        public GetColorQueryHandler(IDeletableEntityRepository<ProductColor> colorsRepository)
        {
            this.colorsRepository = colorsRepository;
        }

        public async Task<ProductColor> Handle(GetColorQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckAssistant.CheckIfProductColorExists(request.Name, this.colorsRepository))
            {
                throw new NotFoundException(nameof(ProductColor), request.Name);
            }

            return await this.colorsRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
        }
    }
}

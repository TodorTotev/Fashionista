namespace Fashionista.Application.ProductAttributes.Queries.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.ProductAttributes.Commands.Create;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateProductAttributeQueryHandler : IRequestHandler<CreateProductAttributeQuery, CreateProductAttributeCommand>
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IMapper mapper;

        public CreateProductAttributeQueryHandler(
            IDeletableEntityRepository<Product> productRepository,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<CreateProductAttributeCommand> Handle(CreateProductAttributeQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var requestedEntity = await this.productRepository
                    .GetByIdWithDeletedAsync(request.Id)
                                  ?? throw new NotFoundException(nameof(Product), request.Id);

            return new CreateProductAttributeCommand
            {
                ProductId = requestedEntity.Id,
                ProductName = requestedEntity.Name,
                MainCategoryId = 1,
            };
        }
    }
}

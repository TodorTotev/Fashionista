namespace Fashionista.Application.ProductAttributes.Queries.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.ProductAttributes.Commands.Create;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class
        CreateProductAttributeQueryHandler : IRequestHandler<CreateProductAttributeQuery, CreateProductAttributeCommand>
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public CreateProductAttributeQueryHandler(
            IDeletableEntityRepository<Product> productRepository,
            IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.productRepository = productRepository;
            this.subCategoryRepository = subCategoryRepository;
        }

        public async Task<CreateProductAttributeCommand> Handle(
            CreateProductAttributeQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var requestedEntity = await this.productRepository
                                      .All()
                                      .Where(x => x.Id == request.Id)
                                      .SingleOrDefaultAsync(cancellationToken)
                                  ?? throw new NotFoundException(nameof(Product), request.Id);

            var subCategory = await this.subCategoryRepository
                                  .All()
                                  .Where(x => x.Id == requestedEntity.SubCategoryId)
                                  .SingleOrDefaultAsync(cancellationToken)
                              ?? throw new NotFoundException(nameof(Product), request.Id);

            return new CreateProductAttributeCommand
            {
                ProductId = requestedEntity.Id,
                ProductName = requestedEntity.Name,
                MainCategoryId = subCategory.MainCategoryId,
            };
        }
    }
}

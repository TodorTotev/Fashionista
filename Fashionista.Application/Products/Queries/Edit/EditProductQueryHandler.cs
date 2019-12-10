namespace Fashionista.Application.Products.Queries.Edit
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.Products.Commands.Edit;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class EditProductQueryHandler : IRequestHandler<EditProductQuery, EditProductCommand>
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public EditProductQueryHandler(
            IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<EditProductCommand> Handle(EditProductQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var requestedEntity = await this.productRepository
                                      .AllAsNoTracking()
                                      .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                  ?? throw new NotFoundException(nameof(Product), request.Id);

            var command = requestedEntity.To<EditProductCommand>();
            return command;
        }
    }
}

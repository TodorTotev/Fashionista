namespace Fashionista.Application.ProductAttributes.Queries.Edit
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.ProductAttributes.Commands.Edit;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class
        EditProductAttributeQueryHandler : IRequestHandler<EditProductAttributeQuery, EditProductAttributeCommand>
    {
        private readonly IDeletableEntityRepository<ProductAttributes> productAttributesRepository;

        public EditProductAttributeQueryHandler(
            IDeletableEntityRepository<ProductAttributes> productAttributesRepository)
        {
            this.productAttributesRepository = productAttributesRepository;
        }

        public async Task<EditProductAttributeCommand> Handle(
            EditProductAttributeQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var command = await this.productAttributesRepository
                              .AllAsNoTracking()
                              .Where(x => x.Id == request.Id)
                              .To<EditProductAttributeCommand>()
                              .SingleOrDefaultAsync(cancellationToken)
                          ?? throw new NotFoundException(nameof(Product), request.Id);

            return command;
        }
    }
}

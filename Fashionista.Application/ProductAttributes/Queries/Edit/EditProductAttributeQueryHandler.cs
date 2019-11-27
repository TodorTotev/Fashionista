namespace Fashionista.Application.ProductAttributes.Queries.Edit
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.ProductAttributes.Commands.Edit;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class
        EditProductAttributeQueryHandler : IRequestHandler<EditProductAttributeQuery, EditProductAttributeCommand>
    {
        private readonly IDeletableEntityRepository<ProductAttributes> productAttributesRepository;
        private readonly IMapper mapper;

        public EditProductAttributeQueryHandler(
            IDeletableEntityRepository<ProductAttributes> productAttributesRepository,
            IMapper mapper)
        {
            this.productAttributesRepository = productAttributesRepository;
            this.mapper = mapper;
        }

        public async Task<EditProductAttributeCommand> Handle(
            EditProductAttributeQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var product = await this.productAttributesRepository
                              .AllAsNoTracking()
                              .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                          ?? throw new NotFoundException(nameof(Product), request.Id);

            var command = this.mapper.Map<EditProductAttributeCommand>(product);
            return command;
        }
    }
}

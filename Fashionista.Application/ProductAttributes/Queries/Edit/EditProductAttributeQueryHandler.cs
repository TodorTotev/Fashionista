using System.Linq;
using AutoMapper.QueryableExtensions;
using Fashionista.Application.Products.Commands.Edit;

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

            var command = await this.productAttributesRepository
                              .All()
                              .Where(x => x.Id == request.Id)
                              .ProjectTo<EditProductAttributeCommand>(this.mapper.ConfigurationProvider)
                              .SingleOrDefaultAsync(cancellationToken)
                          ?? throw new NotFoundException(nameof(Product), request.Id);
            
            return command;
        }
    }
}

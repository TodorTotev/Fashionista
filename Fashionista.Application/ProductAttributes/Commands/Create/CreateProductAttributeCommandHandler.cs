using Fashionista.Application.Exceptions;
using Fashionista.Application.Infrastructure;

namespace Fashionista.Application.ProductAttributes.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateProductAttributeCommandHandler : IRequestHandler<CreateProductAttributeCommand, ProductAttributes>
    {
        private readonly IDeletableEntityRepository<ProductAttributes> productAttributesRepository;
        private readonly IMapper mapper;

        public CreateProductAttributeCommandHandler(
            IDeletableEntityRepository<ProductAttributes> productAttributesRepository,
            IMapper mapper)
        {
            this.productAttributesRepository = productAttributesRepository;
            this.mapper = mapper;
        }

        public async Task<ProductAttributes> Handle(CreateProductAttributeCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            
            if (await CommonCheckAssistant.CheckIfProductAttributeExists(
                request.ProductColorId,
                request.ProductSizeId,
                request.ProductId,
                this.productAttributesRepository))
            {
                throw new EntityAlreadyExistsException(
                    nameof(Product),
                    "Attribute with same color and size already exists!");
            }

            var attribute = this.mapper.Map<ProductAttributes>(request);

            await this.productAttributesRepository.AddAsync(attribute);
            await this.productAttributesRepository.SaveChangesAsync(cancellationToken);

            return attribute;
        }
    }
}
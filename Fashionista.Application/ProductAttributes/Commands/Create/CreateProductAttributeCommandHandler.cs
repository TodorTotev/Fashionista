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

            var attribute = this.mapper.Map<ProductAttributes>(request);

            await this.productAttributesRepository.AddAsync(attribute);
            await this.productAttributesRepository.SaveChangesAsync(cancellationToken);

            return attribute;
        }
    }
}
namespace Fashionista.Application.ProductAttributes.Commands.Edit
{
    using AutoMapper;
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class EditProductAttributeCommand : IRequest<int>, IHaveCustomMapping
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ProductAttributes, EditProductAttributeCommand>();
        }
    }
}

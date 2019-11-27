namespace Fashionista.Application.ProductAttributes.Commands.Edit
{
    using AutoMapper;
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class EditProductAttributeCommand : IRequest<int>, IHaveCustomMapping
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string ProductColorName { get; set; }

        public string ProductSizeName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ProductAttributes, EditProductAttributeCommand>();
            configuration.CreateMap<ProductAttributes, EditProductAttributeCommand>()
                .ForMember(
                    x => x.ProductColorName,
                    y => y.MapFrom(src => src.ProductColor.Name));
        }
    }
}
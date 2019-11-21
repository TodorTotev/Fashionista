namespace Fashionista.Application.ProductAttributes.Queries.GetColor
{
    using Fashionista.Domain.Entities;
    using MediatR;

    public class GetColorQuery : IRequest<ProductColor>
    {
        public string Name { get; set; }
    }
}
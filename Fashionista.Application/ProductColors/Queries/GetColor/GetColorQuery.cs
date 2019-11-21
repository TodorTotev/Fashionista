namespace Fashionista.Application.ProductColors.Queries.GetColor
{
    using Fashionista.Domain.Entities;
    using MediatR;

    public class GetColorQuery : IRequest<ProductColor>
    {
        public string Name { get; set; }
    }
}
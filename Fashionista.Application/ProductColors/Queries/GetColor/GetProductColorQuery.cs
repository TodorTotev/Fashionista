namespace Fashionista.Application.ProductColors.Queries.GetColor
{
    using Fashionista.Domain.Entities;
    using MediatR;

    public class GetProductColorQuery : IRequest<ProductColor>
    {
        public int Id { get; set; }
    }
}

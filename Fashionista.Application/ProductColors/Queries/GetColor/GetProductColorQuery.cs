namespace Fashionista.Application.ProductColors.Queries.GetColor
{
    using Fashionista.Application.Common.Models;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class GetProductColorQuery : IRequest<ProductColorLookupModel>
    {
        public int Id { get; set; }
    }
}

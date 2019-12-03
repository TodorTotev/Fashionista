namespace Fashionista.Application.ProductColors.Queries.GetAllColors
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;
    using MediatR;

    public class GetAllColorsQuery : IRequest<List<ProductColorLookupModel>>
    {
    }
}

namespace Fashionista.Application.ProductSizes.Commands.Create
{
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Application.MainCategories.Queries.GetAllMainCategoriesSelectList;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateProductSizeCommand : IRequest<int>, IMapTo<ProductSize>
    {
        public string Name { get; set; }

        public int MainCategoryId { get; set; }

        public GetAllMainCategoriesSelectListViewModel MainCategoriesSelectList { get; set; }

        public int ProductId { get; set; }
    }
}

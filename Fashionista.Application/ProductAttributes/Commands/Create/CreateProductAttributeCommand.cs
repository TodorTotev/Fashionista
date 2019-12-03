namespace Fashionista.Application.ProductAttributes.Commands.Create
{
    using Fashionista.Application.ProductColors.Queries.GetAllColorsSelectList;
    using Fashionista.Application.ProductSizes.Queries.GetAllSizesSelectList;
    using MediatR;

    public class CreateProductAttributeCommand : IRequest<int>
    {
        public string ProductName { get; set; }

        public int ProductColorId { get; set; }

        public int ProductSizeId { get; set; }

        public int ProductId { get; set; }

        public int MainCategoryId { get; set; }

        public int Quantity { get; set; }

        public GetAllSizesSelectListViewModel SizesSelectListViewModel { get; set; }

        public GetAllColorsSelectListViewModel ColorsSelectListViewModel { get; set; }
    }
}

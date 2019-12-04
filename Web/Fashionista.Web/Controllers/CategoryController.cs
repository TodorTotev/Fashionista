// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Fashionista.Application.Brands.Queries.GetAllBrands;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Common.Models.Category;
    using Fashionista.Application.ProductColors.Queries.GetAllColors;
    using Fashionista.Application.Products.Commands.Sort;
    using Fashionista.Application.Products.Queries.GetAllProductsByCategory;
    using Fashionista.Application.ProductSizes.Queries.GetAllSizesByCategory;
    using Fashionista.Application.SubCategories.Queries.Details;
    using Fashionista.Domain.Entities.Enums;
    using Microsoft.AspNetCore.Mvc;
    using X.PagedList;

    public class CategoryController : BaseController
    {
        public async Task<IActionResult> Index(CategoryProductsViewModel model)
        {
            model.Page = (model.Page ?? 1) - 1;
            const int pageSize = 20;

            var products = await this.Mediator.Send(new GetAllProductsByCategoryQuery { Id = model.Id });

            var subCategory = await this.Mediator.Send(new GetSubCategoryDetailsQuery { Id = model.Id });

            var sizes = await this.Mediator.Send(new GetAllSizesByCategoryQuery
                { Id = subCategory.MainCategoryId });

            var colors = await this.Mediator.Send(new GetAllColorsQuery());

            var brands = await this.Mediator.Send(new GetAllBrandsQuery());

            var sortedProducts = await this.Mediator.Send(new SortProductsCommand
            {
                BrandId = model.BrandId, ColorId = model.ColorId, SizeId = model.SizeId,
                Sort = model.ProductSort, Gender = model.SortGender, Products = products,
                PageNumber = model.Page.Value, PageSize = pageSize,
            });

            var pagedList = new StaticPagedList<ProductLookupModel>(
                sortedProducts, model.Page.Value + 1, pageSize, sortedProducts.Count);

            model.SubCategory = subCategory;
            model.Colors = colors;
            model.Sizes = sizes;
            model.Products = pagedList;
            model.ListOfBrands = brands;

            return this.View(model);
        }

        public async Task<IActionResult> Men(CategoryProductsViewModel model)
        {
            model.Page = (model.Page ?? 1) - 1;
            const int pageSize = 20;

            var products = await this.Mediator.Send(new GetAllProductsByCategoryQuery { Id = model.Id });
            products = products.Where(x => x.ProductType == ProductType.Men).ToList();

            var subCategory = await this.Mediator.Send(new GetSubCategoryDetailsQuery { Id = model.Id });

            var sizes = await this.Mediator.Send(new GetAllSizesByCategoryQuery
                { Id = subCategory.MainCategoryId });

            var colors = await this.Mediator.Send(new GetAllColorsQuery());

            var brands = await this.Mediator.Send(new GetAllBrandsQuery());

            var sortedProducts = await this.Mediator.Send(new SortProductsCommand
            {
                BrandId = model.BrandId, ColorId = model.ColorId, SizeId = model.SizeId,
                Sort = model.ProductSort, Gender = model.SortGender, Products = products,
                PageNumber = model.Page.Value, PageSize = pageSize,
            });

            var pagedList = new StaticPagedList<ProductLookupModel>(
                sortedProducts, model.Page.Value + 1, pageSize, sortedProducts.Count);

            model.SubCategory = subCategory;
            model.Colors = colors;
            model.Sizes = sizes;
            model.Products = pagedList;
            model.ListOfBrands = brands;

            return this.View(model);
        }

        public async Task<IActionResult> Women(CategoryProductsViewModel model)
        {
            model.Page = (model.Page ?? 1) - 1;
            const int pageSize = 20;

            var products = await this.Mediator.Send(new GetAllProductsByCategoryQuery { Id = model.Id });
            products = products.Where(x => x.ProductType == ProductType.Women).ToList();

            var subCategory = await this.Mediator.Send(new GetSubCategoryDetailsQuery { Id = model.Id });

            var sizes = await this.Mediator.Send(new GetAllSizesByCategoryQuery
                { Id = subCategory.MainCategoryId });

            var colors = await this.Mediator.Send(new GetAllColorsQuery());

            var brands = await this.Mediator.Send(new GetAllBrandsQuery());

            var sortedProducts = await this.Mediator.Send(new SortProductsCommand
            {
                BrandId = model.BrandId, ColorId = model.ColorId, SizeId = model.SizeId,
                Sort = model.ProductSort, Gender = model.SortGender, Products = products,
                PageNumber = model.Page.Value, PageSize = pageSize,
            });

            var pagedList = new StaticPagedList<ProductLookupModel>(
                sortedProducts, model.Page.Value + 1, pageSize, sortedProducts.Count);

            model.SubCategory = subCategory;
            model.Colors = colors;
            model.Sizes = sizes;
            model.Products = pagedList;
            model.ListOfBrands = brands;

            return this.View(model);
        }
    }
}

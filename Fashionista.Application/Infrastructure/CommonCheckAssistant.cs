namespace Fashionista.Application.Infrastructure
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    [SuppressMessage("ReSharper", "SA1124", Justification = "Just like this")]
    internal static class CommonCheckAssistant
    {
        #region Categories

        internal static async Task<bool> CheckIfMainCategoryWithSameNameExists(
            string name,
            IDeletableEntityRepository<MainCategory> mainCategoryRepository)
        {
            return await mainCategoryRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }

        internal static async Task<bool> CheckIfSubCategoryWithSameNameExists(
            string name,
            IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            return await subCategoryRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }

        #endregion

        #region Brands

        internal static async Task<bool> CheckIfBrandExists(
            int brandId,
            IDeletableEntityRepository<Brand> brandRepository)
        {
            return await brandRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Id == brandId);
        }

        internal static async Task<bool> CheckIfBrandWithSameNameExists(
            string name,
            IDeletableEntityRepository<Brand> brandRepository)
        {
            return await brandRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }

        #endregion

        internal static async Task<bool> CheckIfProductWithSameNameExists(
            string name,
            IDeletableEntityRepository<Product> productRepository)
        {
            return await productRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }

        #region ProductAttributes

        internal static async Task<bool> CheckIfProductColorExists(
            int id,
            IDeletableEntityRepository<ProductColor> colorsRepository)
        {
            return await colorsRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Id == id);
        }

        internal static async Task<bool> CheckIfProductSizeExists(
            int id,
            IDeletableEntityRepository<ProductSize> sizesRepository)
        {
            return await sizesRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Id == id);
        }

        internal static async Task<bool> CheckIfProductColorWithSameNameExists(
            string name,
            IDeletableEntityRepository<ProductColor> colorsRepository)
        {
            return await colorsRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }

        internal static async Task<bool> CheckIfProductSizeWithSameNameExists(
            string name,
            IDeletableEntityRepository<ProductSize> sizesRepository)
        {
            return await sizesRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }

        internal static async Task<bool> CheckIfProductAttributeExists(
            int colorId,
            int sizeId,
            int productId,
            IDeletableEntityRepository<ProductAttributes> productRepository)
        {
            return await productRepository
                .All()
                .AnyAsync(x => x.ProductId == productId &&
                               x.ProductSizeId == sizeId
                               && x.ProductColorId == colorId);
        }

        #endregion
    }
}
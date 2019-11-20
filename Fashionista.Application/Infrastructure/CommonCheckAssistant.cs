namespace Fashionista.Application.Infrastructure
{
    using System.Threading.Tasks;

    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    internal static class CommonCheckAssistant
    {
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

        internal static async Task<bool> CheckIfBrandWithSameNameExists(
            string name,
            IDeletableEntityRepository<Brand> brandRepository)
        {
            return await brandRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }
    }
}

namespace Fashionista.Application.Tests.MainCategories.Commands.Edit
{
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.MainCategories.Commands.Edit;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using Xunit;

    public class EditMainCategoryCommandTests : BaseTest<MainCategory>
    {
        [Trait(nameof(MainCategory), "EditMainCategory command tests")]
        [Fact(DisplayName = "Handle given valid request should edit main category")]
        public async Task Handle_GivenValidRequest_ShouldEditMainCategory()
        {
            // Arrange
            await this.deletableEntityRepository.AddAsync(new MainCategory
            {
                Name = "TestCategory",
            });
            await this.deletableEntityRepository.SaveChangesAsync();

            var command = new EditMainCategoryCommand
            {
                Id = 1,
                Name = "ModifiedCategory",
            };

            var sut = new EditMainCategoryCommandHandler(this.deletableEntityRepository);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var modifiedCategory = this.dbContext.MainCategories
                .FirstOrDefaultAsync(x => x.Id == id);

            modifiedCategory.ShouldNotBeNull();
        }
    }
}
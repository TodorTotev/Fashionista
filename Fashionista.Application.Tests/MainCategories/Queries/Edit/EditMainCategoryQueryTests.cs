namespace Fashionista.Application.Tests.MainCategories.Queries.Edit
{
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.MainCategories.Commands.Edit;
    using Fashionista.Application.MainCategories.Queries.Edit;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class EditMainCategoryQueryTests : BaseTest<MainCategory>
    {
        [Trait(nameof(MainCategory), "EditMainCategory query tests")]
        [Fact(DisplayName = "Handle given valid request should return EditMainCategoryCommand")]
        public async Task Handle_GivenValidRequest_ShouldReturnEditMainCategoryCommand()
        {
            // Arrange
            var query = new EditMainCategoryQuery { Id = 1 };
            var sut = new EditMainCategoryQueryHandler(this.deletableEntityRepository, this.mapper);

            await this.deletableEntityRepository.AddAsync(new MainCategory
            {
                Name = "TestCategory",
            });
            await this.deletableEntityRepository.SaveChangesAsync();

            // Act
            var command = await sut.Handle(query, It.IsAny<CancellationToken>());

            command.ShouldNotBeNull();
            command.ShouldBeOfType<EditMainCategoryCommand>();
            command.Name.ShouldBe("TestCategory");
        }
    }
}
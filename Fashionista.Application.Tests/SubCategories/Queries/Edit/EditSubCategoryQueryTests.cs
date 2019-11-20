using System.Threading;
using Fashionista.Application.SubCategories.Commands.Edit;
using Moq;
using Shouldly;

namespace Fashionista.Application.Tests.SubCategories.Queries.Edit
{
    using System.Threading.Tasks;
    using Fashionista.Application.SubCategories.Queries.Edit;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Xunit;

    public class EditSubCategoryQueryTests : BaseTest<SubCategory>
    {
        [Trait(nameof(MainCategory), "EditSubCategory query tests")]
        [Fact(DisplayName = "Handle given valid request should return EditSubCategoryCommand")]
        public async Task Handle_GivenValidRequest_ShouldReturnEditSubCategoryCommand()
        {
            // Arrange
            var query = new EditSubCategoryQuery { Id = 1 };
            var sut = new EditSubCategoryQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var command = await sut.Handle(query, It.IsAny<CancellationToken>());

            command.ShouldNotBeNull();
            command.ShouldBeOfType<EditSubCategoryCommand>();
            command.Name.ShouldBe("Category1");
            command.Description.ShouldBe("TestDesc");
        }
    }
}
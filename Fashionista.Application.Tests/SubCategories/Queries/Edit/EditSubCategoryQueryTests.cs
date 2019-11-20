namespace Fashionista.Application.Tests.SubCategories.Queries.Edit
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.SubCategories.Commands.Edit;
    using Fashionista.Application.SubCategories.Queries.Edit;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
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

        [Trait(nameof(SubCategory), "EditSubCategory query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new EditSubCategoryQuery() { Id = 1000 };
            var sut = new EditSubCategoryQueryHandler(this.deletableEntityRepository, It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
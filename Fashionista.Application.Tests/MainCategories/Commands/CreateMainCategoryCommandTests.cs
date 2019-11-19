namespace Fashionista.Application.Tests.MainCategories.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.MainCategories.Commands.Create;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CreateMainCategoryCommandTests : BaseTest<MainCategory>
    {
        [Trait(nameof(MainCategory), "CreateMainCategory command tests")]
        [Fact(DisplayName = "Request should create MainCategory")]
        public async Task Handle_ShouldCreateMainCategory()
        {
            // Arrange
            var command = new CreateMainCategoryCommand
            {
                Name = "ValidCategory",
            };

            var categoryRepository = new EfDeletableEntityRepository<MainCategory>(this.dbContext);

            var test = new CreateMainCategoryCommandHandler(categoryRepository, this.mapper);

            // Act
            var id = await test.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var createdCategory = this.deletableEntityRepository
                .AllAsNoTracking()
                .SingleOrDefault(x => x.Name == "ValidCategory");

            createdCategory.Name.ShouldBe("ValidCategory");
            createdCategory.SubCategories.Count.ShouldBe(0);
        }

        [Trait(nameof(MainCategory), "CreateMainCategory command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            var test = new CreateMainCategoryCommandHandler(
                It.IsAny<IDeletableEntityRepository<MainCategory>>(),
                It.IsAny<IMapper>());

            await Should.ThrowAsync<ArgumentNullException>(test.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
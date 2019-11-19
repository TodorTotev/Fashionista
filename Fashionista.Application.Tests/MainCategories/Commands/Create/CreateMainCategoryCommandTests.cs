namespace Fashionista.Application.Tests.MainCategories.Commands.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Fashionista.Application.Exceptions;
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

            var mainCategoryRepository = new EfDeletableEntityRepository<MainCategory>(this.dbContext);

            var test = new CreateMainCategoryCommandHandler(mainCategoryRepository, this.mapper);

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
            var sut = new CreateMainCategoryCommandHandler(
                It.IsAny<IDeletableEntityRepository<MainCategory>>(),
                It.IsAny<IMapper>());

            await Should.ThrowAsync<ArgumentNullException>(
                sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(MainCategory), "CreateMainCategory command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var command = new CreateMainCategoryCommand
            {
                Name = "Category1",
            };

            var mainCategoryRepository = new EfDeletableEntityRepository<MainCategory>(this.dbContext);

            var sut = new CreateMainCategoryCommandHandler(mainCategoryRepository, this.mapper);

            // Act & Assert
            await sut.Handle(command, It.IsAny<CancellationToken>());
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
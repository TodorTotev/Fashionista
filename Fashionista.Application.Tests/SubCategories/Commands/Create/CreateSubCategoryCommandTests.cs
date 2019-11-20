// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.SubCategories.Commands.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.SubCategories.Commands.Create;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CreateSubCategoryCommandTests : BaseTest<SubCategory>
    {
        [Trait(nameof(SubCategory), "CreateSubCategory command tests")]
        [Fact(DisplayName = "Request should create SubCategory")]
        public async Task Handle_ShouldCreateSubCategory()
        {
            // Arrange
            var command = new CreateSubCategoryCommand
            {
                Name = "ValidCategory",
                Description = "ValidDescription",
                MainCategoryId = 1,
            };
            var test = new CreateSubCategoryCommandHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var id = await test.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var createdCategory = this.deletableEntityRepository
                .AllAsNoTracking()
                .SingleOrDefault(x => x.Name == "ValidCategory");

            id.ShouldBe(4);
            createdCategory.Name.ShouldBe("ValidCategory");
            createdCategory.Description.ShouldBe("ValidDescription");
            createdCategory.MainCategoryId.ShouldBe(1);
        }

        [Trait(nameof(SubCategory), "CreateSubCategory command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            var sut = new CreateSubCategoryCommandHandler(
                It.IsAny<IDeletableEntityRepository<SubCategory>>(),
                It.IsAny<IMapper>());

            await Should.ThrowAsync<ArgumentNullException>(
                sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(SubCategory), "CreateSubCategory command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var command = new CreateSubCategoryCommand
            {
                Name = "SubCategory1",
                Description = "TestDesc",
                MainCategoryId = 1,
            };

            var sut = new CreateSubCategoryCommandHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await sut.Handle(command, It.IsAny<CancellationToken>());
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
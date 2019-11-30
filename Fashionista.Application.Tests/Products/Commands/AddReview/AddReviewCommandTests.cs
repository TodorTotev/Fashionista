// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.Products.Commands.AddReview
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Products.Commands.AddReview;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class AddReviewCommandTests : BaseTest<Product>
    {
        [Trait(nameof(Product), "AddReview command tests")]
        [Fact(DisplayName = "Handle given valid request should add review")]
        public async Task Handle_GivenValidRequest_ShouldAddReview()
        {
            // Arrange
            var command = new AddReviewCommand { Id = 1, Rating = 5 };
            var sut = new AddReviewCommandHandler(this.deletableEntityRepository);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            id.ShouldBe(1);

            var product = this.dbContext.Products.FirstOrDefault(x => x.Id == id);
            product.Name.ShouldBe("ActiveProduct");
            product.Reviews.Count().ShouldBe(1);
        }

        [Trait(nameof(Product), "EditProduct query tests")]
        [Fact(DisplayName = "Handle given invalid request should return ArgumentOutOfRangeException ")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var query = new AddReviewCommand { Id = 1, Rating = 6 };
            var sut = new AddReviewCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentOutOfRangeException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Product), "AddReview command tests")]
        [Fact(DisplayName = "Handle given invalid request should return NotFoundException ")]
        public async Task Handle_GivenInvalidRequest_ShouldReturnNotFoundException()
        {
            // Arrange
            var query = new AddReviewCommand { Id = 1000, Rating = 2 };
            var sut = new AddReviewCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Product), "AddReview command tests")]
        [Fact(DisplayName = "Handle given null request should return ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldReturnArgumentNullException()
        {
            // Arrange
            var sut = new AddReviewCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
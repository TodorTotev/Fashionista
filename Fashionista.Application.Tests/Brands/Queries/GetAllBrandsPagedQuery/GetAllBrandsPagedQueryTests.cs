namespace Fashionista.Application.Tests.Brands.Queries.GetAllBrandsPagedQuery
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Brands.Queries.GetAllBrandsPaged;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllBrandsPagedQueryTests : BaseTest<Brand>
    {
        [Trait(nameof(Brand), "GetAllBrandsPaged query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetAllBrandsPagedQuery { PageNumber = 0, PageSize = 3 };
            var sut = new GetAllBrandsPagedQueryHandler(this.deletableEntityRepository);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeOfType<GetAllBrandsPagedViewModel>();
            viewModel.Brands.Count().ShouldBe(1);
        }

        [Trait(nameof(Brand), "GetAllBrandsPaged query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllBrandsPagedQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
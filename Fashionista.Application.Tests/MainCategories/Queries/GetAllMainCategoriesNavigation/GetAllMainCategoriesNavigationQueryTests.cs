 namespace Fashionista.Application.Tests.MainCategories.Queries.GetAllMainCategoriesNavigation
 {
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.MainCategories.Queries.GetAllMainCategoriesNavigation;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllMainCategoriesNavigationQueryTests : BaseTest<MainCategory>
     {
         [Trait(nameof(MainCategory), "GetAllMainCategoriesNavigation query tests")]
         [Fact(DisplayName = "Handle given valid request should return view model")]
         public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
         {
             // Arrange
             var query = new GetAllMainCategoriesNavigationQuery();
             var sut = new GetAllMainCategoriesNavigationQueryHandler(this.deletableEntityRepository, this.mapper);

             // Act
             var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

             // Assert
             viewModel.ShouldNotBeNull();
             viewModel.ShouldBeOfType<CategoriesNavigationViewModel>();
             viewModel.Categories.Count().ShouldBe(3);
             viewModel.Categories.FirstOrDefault()?.SubCategories.ShouldNotBeNull();
             viewModel.Categories.FirstOrDefault()?.SubCategories.Count().ShouldBe(3);
         }

         [Trait(nameof(MainCategory), "GetAllMainCategoriesNavigation query tests")]
         [Fact(DisplayName = "Handle given valid null request should throw ArgumentNullException")]
         public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
         {
             // Arrange
             var sut = new GetAllMainCategoriesNavigationQueryHandler(this.deletableEntityRepository, this.mapper);

             // Assert
             await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
         }
     }
 }
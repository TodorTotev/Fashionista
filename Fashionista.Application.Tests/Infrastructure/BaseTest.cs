// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable PossibleNullReferenceException

using Fashionista.Domain.Entities;

namespace Fashionista.Application.Tests.Infrastructure
{
    // ReSharper disable SA1401
    using System;
    using System.Linq;
    using AutoMapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Infrastructure;
    using Fashionista.Persistence;
    using Fashionista.Persistence.Repositories;
    using MediatR;
    using Moq;

    public abstract class BaseTest<T> : IDisposable
        where T : class, IDeletableEntity, new()
    {
        protected readonly ApplicationDbContext dbContext;
        protected readonly IMapper mapper;
        protected readonly Mock<IMediator> mediatorMock;
        protected readonly IDeletableEntityRepository<T> deletableEntityRepository;
        protected readonly Mock<IUserAssistant> userAssistantMock;
        protected readonly Mock<IShoppingCartAssistant> shoppingCartAssistantMock;

        protected BaseTest()
        {
            this.dbContext = ApplicationDbContextFactory.Create();
            this.mapper = AutoMapperFactory.Create();
            this.deletableEntityRepository = new EfDeletableEntityRepository<T>(this.dbContext);
            this.mediatorMock = new Mock<IMediator>();
            this.userAssistantMock = UserAssistantFactory.Create(
                this.User.ShoppingCartId,
                this.User.Id,
                this.User.FirstName,
                this.User.LastName,
                this.User.PhoneNumber);
            this.shoppingCartAssistantMock = ShoppingCartAssistantFactory.Create(this.dbContext.Products.ToList());

            MapperInitializer.InitializeMapper();
        }

        public ApplicationUser User => this.dbContext.Users.FirstOrDefault();

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(this.dbContext);
            this.deletableEntityRepository.Dispose();
        }
    }
}
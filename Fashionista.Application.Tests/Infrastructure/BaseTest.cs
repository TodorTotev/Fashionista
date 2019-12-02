// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable PossibleNullReferenceException

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

        protected BaseTest()
        {
            this.dbContext = ApplicationDbContextFactory.Create();
            this.mapper = AutoMapperFactory.Create();
            this.deletableEntityRepository = new EfDeletableEntityRepository<T>(this.dbContext);
            this.mediatorMock = new Mock<IMediator>();
            this.userAssistantMock = UserAssistantFactory.Create(this.dbContext.Users.FirstOrDefault().ShoppingCartId);
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(this.dbContext);
            this.deletableEntityRepository.Dispose();
        }
    }
}
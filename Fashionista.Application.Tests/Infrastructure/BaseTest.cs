// ReSharper disable MemberCanBePrivate.Global

namespace Fashionista.Application.Tests.Infrastructure
{
    using System;
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

        protected BaseTest()
        {
            this.dbContext = ApplicationDbContextFactory.Create();
            this.mapper = AutoMapperFactory.Create();
            this.deletableEntityRepository = new EfDeletableEntityRepository<T>(this.dbContext);
            this.mediatorMock = new Mock<IMediator>();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(this.dbContext);
            this.deletableEntityRepository.Dispose();
        }
    }
}
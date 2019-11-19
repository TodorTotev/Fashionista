// ReSharper disable MemberCanBePrivate.Global

using Fashionista.Domain.Infrastructure;

namespace Fashionista.Application.Tests.Infrastructure
{
    using System;
    using Fashionista.Persistence;
    using Fashionista.Persistence.Interfaces;
    using Fashionista.Persistence.Repositories;
    using AspNetCoreTemplate.Data.Common.Models;
    using AutoMapper;
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
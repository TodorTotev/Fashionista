// ReSharper disable MethodSupportsCancellation

namespace Fashionista.Application.Addresses.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using static Fashionista.Common.GlobalConstants;

    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, int>
    {
        private readonly IDeletableEntityRepository<Address> addressesRepository;
        private readonly IMediator mediator;

        public DeleteAddressCommandHandler(
            IDeletableEntityRepository<Address> addressesRepository,
            IMediator mediator)
        {
            this.addressesRepository = addressesRepository;
            this.mediator = mediator;
        }

        public async Task<int> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException();

            var requestedEntity = await this.addressesRepository
                                      .AllWithDeleted()
                                      .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                  ?? throw new NotFoundException(nameof(Address), request.Id);

            if (requestedEntity.IsDeleted)
            {
                throw new FailedDeletionException(nameof(Address), request.Id, EntityAlreadyDeletedMessage);
            }

            this.addressesRepository.Delete(requestedEntity);
            await this.addressesRepository.SaveChangesAsync(cancellationToken);

            await this.mediator.Publish(new AddressDeletedNotification { AddressName = requestedEntity.Name });

            return requestedEntity.Id;
        }
    }
}

namespace Fashionista.Application.Addresses.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;

    using static Fashionista.Common.GlobalConstants;

    public class AddressCreatedNotification : INotification
    {
        public string AddressName { get; set; }

        public class Handler : INotificationHandler<AddressCreatedNotification>
        {
            private readonly IDeletableEntityRepository<Notification> notificationRepository;
            private readonly IUserAssistant userAssistant;

            public Handler(IDeletableEntityRepository<Notification> notificationRepository, IUserAssistant userAssistant)
            {
                this.notificationRepository = notificationRepository;
                this.userAssistant = userAssistant;
            }

            public async Task Handle(AddressCreatedNotification notification, CancellationToken cancellationToken)
            {
                var entity = new Notification
                {
                    ApplicationUserId = this.userAssistant.UserId,
                    Header = notification.AddressName,
                    Content = CreatedNotificationMsg,
                    Type = NotificationType.Success,
                };

                await this.notificationRepository.AddAsync(entity);
                await this.notificationRepository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}

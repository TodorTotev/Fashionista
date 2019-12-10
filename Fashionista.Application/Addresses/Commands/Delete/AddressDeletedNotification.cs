namespace Fashionista.Application.Addresses.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;

    using static Fashionista.Common.GlobalConstants;

    public class AddressDeletedNotification : INotification
    {
        public string AddressName { get; set; }

        public class Handler : INotificationHandler<AddressDeletedNotification>
        {
            private readonly IDeletableEntityRepository<Notification> notificationRepository;
            private readonly IUserAssistant userAssistant;
            private readonly INotifyService notifyService;

            public Handler(
                IDeletableEntityRepository<Notification> notificationRepository,
                IUserAssistant userAssistant,
                INotifyService notifyService)
            {
                this.notificationRepository = notificationRepository;
                this.userAssistant = userAssistant;
                this.notifyService = notifyService;
            }

            public async Task Handle(AddressDeletedNotification notification, CancellationToken cancellationToken)
            {
                var notificationEntity = new Notification
                {
                    Id = Guid.NewGuid().ToString(),
                    ApplicationUserId = this.userAssistant.UserId,
                    Header = notification.AddressName,
                    Content = DeletedNotificationMsg,
                    Type = NotificationType.Success,
                };

                await this.notificationRepository.AddAsync(notificationEntity);
                await this.notificationRepository.SaveChangesAsync(cancellationToken);

                await this.notifyService.SendUserPushNotification(
                    notification.AddressName,
                    DeletedNotificationMsg,
                    NotificationType.Success.ToString());
            }
        }
    }
}

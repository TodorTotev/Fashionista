namespace Fashionista.Application.Addresses.Commands.Create
{
    using System;
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

            public async Task Handle(AddressCreatedNotification notification, CancellationToken cancellationToken)
            {
                var notificationEntity = new Notification
                {
                    Id = Guid.NewGuid().ToString(),
                    ApplicationUserId = this.userAssistant.UserId,
                    Header = notification.AddressName,
                    Content = CreatedNotificationMsg,
                    Type = NotificationType.Success,
                };

                await this.notificationRepository.AddAsync(notificationEntity);
                await this.notificationRepository.SaveChangesAsync(cancellationToken);

                await this.notifyService.SendUserPushNotification(
                    notification.AddressName,
                    CreatedNotificationMsg,
                    NotificationType.Success.ToString());
            }
        }
    }
}

namespace Fashionista.Infrastructure.Hubs
{
    using System.Threading.Tasks;

    using Fashionista.Application.Interfaces;
    using Microsoft.AspNetCore.SignalR;

    public class UserNotificationHub : Hub, INotifyService
    {
        private readonly IUserAssistant userAssistant;
        private readonly IHubContext<UserNotificationHub> hubContext;

        public UserNotificationHub(IUserAssistant userAssistant, IHubContext<UserNotificationHub> hubContext)
        {
            this.userAssistant = userAssistant;
            this.hubContext = hubContext;
        }

        public async Task SendUserPushNotification(string header, string message, string type, string userId = null)
        {
            await this.hubContext
             .Clients
             .User(userId ?? this.userAssistant.UserId)
             .SendAsync("ReceivePushNotification", header, message, type);
        }
    }
}

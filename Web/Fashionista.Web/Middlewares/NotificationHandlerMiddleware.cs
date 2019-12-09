namespace Fashionista.Web.Middlewares
{
    using System.Threading.Tasks;

    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Microsoft.AspNetCore.Http;

    public class NotificationHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IRedisService<Notification> redisNotificationService;

        public NotificationHandlerMiddleware(
            RequestDelegate next,
            IRedisService<Notification> redisNotificationService)
        {
            this.next = next;
            this.redisNotificationService = redisNotificationService;
        }

        public async Task InvokeAsync(HttpContext context, IUserAssistant userAssistant, INotifyService notifyService)
        {
            await this.next(context);

            if (context.User.Identity.IsAuthenticated && context.Response.StatusCode == StatusCodes.Status204NoContent)
            {
                var notification = await this.redisNotificationService.Get(userAssistant.UserId);

                if (notification != null)
                {
                    await notifyService.SendUserPushNotification(
                        notification.Header, notification.Content, notification.Type.ToString());
                }
            }
        }
    }
}
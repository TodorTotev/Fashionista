namespace Fashionista.Application.Infrastructure
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class CustomExceptionNotificationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger logger;
        private readonly IUserAssistant userAssistant;
        private readonly IRedisService<Notification> redisService;

        public CustomExceptionNotificationBehaviour(
            ILogger<TRequest> logger,
            IUserAssistant userAssistant,
            IRedisService<Notification> redisService)
        {
            this.logger = logger;
            this.userAssistant = userAssistant;
            this.redisService = redisService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;

            try
            {
                response = await next();
            }
            catch (BaseCustomException exception)
            {
                this.logger.LogError(
                    "Fashionista Request: {Name}-[UserId: {userId}] {@Request}, {@exception}",
                    typeof(TRequest).Name, this.userAssistant.UserId, request, exception);

                if (this.userAssistant.UserId != null && !(exception is NotFoundException) && !(exception is ForbiddenException))
                {
                    var notification = new Notification()
                    {
                        Type = NotificationType.Error,
                        Header = "Error",
                        ApplicationUserId = this.userAssistant.UserId,
                        Content = HttpUtility.HtmlEncode(exception.Message),
                    };

                    await this.redisService.Save(this.userAssistant.UserId, notification, TimeSpan.FromMinutes(2));
                }

                throw exception;
            }

            return response;
        }
    }
}
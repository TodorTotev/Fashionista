namespace Fashionista.Domain.Entities
{
    using Fashionista.Domain.Entities.Enums;
    using Fashionista.Domain.Infrastructure;

    public class Notification : BaseDeletableModel<string>
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser Player { get; set; }

        public string Header { get; set; }

        public string Content { get; set; }

        public NotificationType Type { get; set; }
    }
}

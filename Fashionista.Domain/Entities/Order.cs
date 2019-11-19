namespace Fashionista.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using Fashionista.Domain.Entities.Enums;
    using Fashionista.Domain.Infrastructure;

    public class Order : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public OrderState OrderState { get; set; }

        public PaymentState PaymentState { get; set; }

        public PaymentType PaymentType { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public DateTime? DateSent { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DeliveryFee { get; set; }

        public string Recipient { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public string InvoiceNumber { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int? DeliveryAddressId { get; set; }

        public virtual Address DeliveryAddress { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}

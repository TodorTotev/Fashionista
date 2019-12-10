namespace Fashionista.Application.Common.Models
{
    using System;

    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;

    public class OrderLookupModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DeliveryFee { get; set; }

        public PaymentType PaymentType { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? DateSent { get; set; }

        public string Recipient { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public string DeliveryAddressName { get; set; }

        public string DeliveryAddressDescription { get; set; }

        public string DeliveryAddressCityName { get; set; }

        public string DeliveryAddressCityPostCode { get; set; }
    }
}

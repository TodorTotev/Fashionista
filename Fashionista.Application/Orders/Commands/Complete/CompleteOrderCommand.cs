namespace Fashionista.Application.Orders.Commands.Complete
{
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;

    public class CompleteOrderCommand : IRequest<int>, IMapFrom<Order>
    {
        public decimal TotalPrice { get; set; }

        public decimal DeliveryFee { get; set; }

        public PaymentType PaymentType { get; set; }

        public string Recipient { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public string DeliveryAddressName { get; set; }

        public string DeliveryAddressDescription { get; set; }

        public string DeliveryAddressCityName { get; set; }

        public string DeliveryAddressCityPostCode { get; set; }
    }
}

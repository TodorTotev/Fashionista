namespace Fashionista.Application.Orders.Commands.Create
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;
    using MediatR;

    public class CreateOrderCommand : IRequest<int>
    {
        public string CustomerInformation { get; set; }
        
        public int? DeliveryAddressId { get; set; }

        public decimal DeliveryFee { get; set; }

        public IEnumerable<AddressLookupModel> Addresses { get; set; }
    }
}

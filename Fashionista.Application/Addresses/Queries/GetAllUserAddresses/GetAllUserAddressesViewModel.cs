namespace Fashionista.Application.Addresses.Queries.GetAllUserAddresses
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class GetAllUserAddressesViewModel
    {
        public IEnumerable<AddressLookupModel> Addresses { get; set; }
    }
}

namespace Fashionista.Application.Addresses.Commands.Create
{
    using MediatR;

    public class CreateAddressCommand : IRequest<int>
    {
        public string Street { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }
    }
}

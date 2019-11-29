namespace Fashionista.Application.Addresses.Commands.Delete
{
    using MediatR;

    public class DeleteAddressCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}

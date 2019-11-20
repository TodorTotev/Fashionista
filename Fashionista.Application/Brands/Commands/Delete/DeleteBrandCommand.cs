namespace Fashionista.Application.Brands.Commands.Delete
{
    using MediatR;

    public class DeleteBrandCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
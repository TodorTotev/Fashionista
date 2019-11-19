namespace Fashionista.Application.MainCategories.Commands.Delete
{
    using MediatR;

    public class DeleteMainCategoryCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}

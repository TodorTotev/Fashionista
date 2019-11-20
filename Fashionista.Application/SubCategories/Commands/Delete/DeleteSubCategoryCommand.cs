namespace Fashionista.Application.SubCategories.Commands.Delete
{
    using MediatR;

    public class DeleteSubCategoryCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}

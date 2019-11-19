namespace Fashionista.Application.MainCategories.Queries.Edit
{
    using Fashionista.Application.MainCategories.Commands.Edit;
    using MediatR;

    public class EditMainCategoryQuery : IRequest<EditMainCategoryCommand>
    {
        public int Id { get; set; }
    }
}
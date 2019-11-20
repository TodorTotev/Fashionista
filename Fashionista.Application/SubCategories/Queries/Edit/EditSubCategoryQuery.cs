namespace Fashionista.Application.SubCategories.Queries.Edit
{
    using Fashionista.Application.SubCategories.Commands.Edit;
    using MediatR;

    public class EditSubCategoryQuery : IRequest<EditSubCategoryCommand>
    {
        public int Id { get; set; }
    }
}

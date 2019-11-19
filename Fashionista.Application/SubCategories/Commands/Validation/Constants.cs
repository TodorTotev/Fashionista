namespace Fashionista.Application.SubCategories.Commands.Validation
{
    public class Constants
    {
        internal const int MinNameLength = 3;
        internal const int MaxNameLength = 10;

        internal const int MinDescLength = 5;
        internal const int MaxDescLength = 20;

        internal const string NameLengthMessage = "Category name length must be between {0} and {1} characters long!";
        internal const string DescLengthMessage = "Category description length must be between {0} and {1} characters long!";
    }
}

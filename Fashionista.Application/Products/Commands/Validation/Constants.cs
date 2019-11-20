namespace Fashionista.Application.Products.Commands.Validation
{
    internal static class Constants
    {
        internal const int NameMinLength = 3;
        internal const int NameMaxLength = 30;

        internal const int DescMinLength = 10;
        internal const int DescMaxLength = 250;

        internal const string NameLengthMsg = "Product name length must be between {0} and {1} characters long!";
        internal const string DescLengthMsg = "Product description length must be between {0} and {1} characters long!";
    }
}
namespace Fashionista.Application.Brands.Commands.Validation
{
    internal static class Constants
    {
        internal const int NameMinLength = 2;
        internal const int NameMaxLength = 15;

        internal const string NameLengthMessage = "Brand name length must be between {0} and {1} characters long!";
    }
}
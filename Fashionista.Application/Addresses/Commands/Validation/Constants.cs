namespace Fashionista.Application.Addresses.Commands.Validation
{
    internal static class Constants
    {
        internal const int MinStreetLength = 3;
        internal const int MaxStreetLength = 20;

        internal const string StreetLengthMessage = "Street name length must be between {0} and {1} characters long!";

        internal const int MinDescLength = 5;
        internal const int MaxDescLength = 25;

        internal const string DescLengthMessage = "Street name length must be between {0} and {1} characters long!";

        internal const int MinCityNameLength = 3;
        internal const int MaxCityNameLength = 15;

        internal const string CityLengthMessage = "Street name length must be between {0} and {1} characters long!";

        internal const int MinZipLength = 2;
        internal const int MaxZipLength = 10;

        internal const string ZipLengthMessage = "Street name length must be between {0} and {1} characters long!";
    }
}

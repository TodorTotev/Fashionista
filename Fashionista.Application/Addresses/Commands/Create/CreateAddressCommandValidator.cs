namespace Fashionista.Application.Addresses.Commands.Create
{
    using Fashionista.Domain.Entities;
    using FluentValidation;
    using Humanizer;

    using static Validation.Constants;

    public class CreateAddressCommandValidator : AbstractValidator<Address>
    {
        public CreateAddressCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .Length(MinStreetLength, MaxStreetLength)
                .WithMessage(StreetLengthMessage.FormatWith(MinStreetLength, MaxStreetLength));

            this.RuleFor(x => x.Description)
                .Length(MinDescLength, MaxDescLength)
                .WithMessage(DescLengthMessage.FormatWith(MinDescLength, MaxDescLength));

            this.RuleFor(x => x.City.Name)
                .Length(MinCityNameLength, MaxCityNameLength)
                .WithMessage(CityLengthMessage.FormatWith(MinCityNameLength, MaxCityNameLength));

            this.RuleFor(x => x.City.Postcode)
                .Length(MinZipLength, MaxZipLength)
                .WithMessage(ZipLengthMessage.FormatWith(MinZipLength, MaxZipLength));
        }
    }
}

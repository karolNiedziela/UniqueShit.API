using UniqueShit.Domain.Enumerations;

namespace UniqueShit.Application.Features.Offers.Queries.GetOffers
{
    public sealed class GetOffersResponse
    {
        public required int Id { get; set; }

        public required string Topic { get; set; }

        public required ManufacturerResponse Manufacturer { get; set; }

        public required MoneyResponse Price { get; set; }

        public required EnumerationResponse ItemCondition { get; set; }

        public required List<EnumerationResponse> Colours { get; set; }

        public required int Quantity { get; set; }
    }

    public sealed record ManufacturerResponse(int Id, string Name);

    public sealed record MoneyResponse(decimal Value, string Currency);

    public sealed record EnumerationResponse(int id, string name);

}

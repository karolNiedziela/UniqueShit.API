using UniqueShit.Application.Core.Responses;

namespace UniqueShit.Application.Features.Offers.Contracts.Responses
{
    public sealed class GetOffersResponse
    {
        public required int Id { get; set; }

        public required string Topic { get; set; }

        public required OfferManufacturerResponse Manufacturer { get; set; }

        public required MoneyResponse Price { get; set; }

        public required EnumerationResponse ItemCondition { get; set; }

        public required List<EnumerationResponse> Colours { get; set; }

        public required int Quantity { get; set; }
    }
}

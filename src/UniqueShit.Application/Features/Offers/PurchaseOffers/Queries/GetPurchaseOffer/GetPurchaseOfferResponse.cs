using UniqueShit.Application.Features.Offers.Contracts.Responses;

namespace UniqueShit.Application.Features.Offers.PurchaseOffers.Queries.GetPurchaseOffer
{
    public sealed class GetPurchaseOfferResponse
    {
        public required int Id { get; set; }

        public required string Topic { get; set; }

        public required string Description { get; set; }

        public required ModelResponse Model { get; set; }
    }
}

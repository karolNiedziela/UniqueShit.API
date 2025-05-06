using UniqueShit.Application.Core.Responses;
using UniqueShit.Application.Features.Offers.Contracts.Responses;

namespace UniqueShit.Application.Features.Offers.SaleOffers.Queries.GetSaleOffer
{
    public sealed class GetSaleOfferResponse
    {
        public required int Id { get; set; }

        public required string Topic { get; set; }

        public required string Description { get; set; }

        public required ModelDetailsResponse Model { get; set; }

        public required MoneyResponse Price { get; set; }

        public required EnumerationResponse ItemCondition { get; set; }

        public required EnumerationResponse PaymentType { get; set; }

        public required EnumerationResponse DeliveryType { get; set; }

        public required int Quantity { get; set; }
    }
}

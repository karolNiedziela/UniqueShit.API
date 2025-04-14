using UniqueShit.Application.Core.Responses;
using UniqueShit.Application.Features.Offers.Contracts.Responses;

namespace UniqueShit.Application.Features.Offers.Queries.GetOffer
{
    public sealed class GetOfferResponse
    {
        public required int Id { get; set; }

        public required string Topic { get; set; }

        public required string Description { get; set; }

        public required BrandResponse Brand { get; set; }

        public required MoneyResponse Price { get; set; }

        public required EnumerationResponse ItemCondition { get; set; }

        public required int Quantity { get; set; }
    }
}

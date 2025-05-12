using UniqueShit.Application.Features.Offers.Contracts.Responses;

namespace UniqueShit.Application.Features.Offers.PurchaseOffers.Queries.GetPurchaseOffers
{
    public sealed record GetPurchaseOffersResponse(
        int Id,
        string Topic,
        BrandResponse Brand,
        ModelResponse Model);
}

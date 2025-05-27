using UniqueShit.Application.Features.Offers.Contracts.Responses;

namespace UniqueShit.Application.Features.Offers.PurchaseOffers.Queries.GetPurchaseOffers
{
    public sealed record GetPurchaseOffersResponse(
        int Id,
        string Topic,
        string Description,
        BrandResponse Brand,
        ModelResponse Model);
}

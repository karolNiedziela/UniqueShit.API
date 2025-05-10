using UniqueShit.Application.Core.Messaging;

namespace UniqueShit.Application.Features.Offers.PurchaseOffers.Queries.GetPurchaseOffer
{
    public sealed record GetPurchaseOfferQuery(int Id) : IQuery<GetPurchaseOfferResponse?>;
}

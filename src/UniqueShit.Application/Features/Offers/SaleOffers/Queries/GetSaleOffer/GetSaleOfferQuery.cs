using UniqueShit.Application.Core.Messaging;

namespace UniqueShit.Application.Features.Offers.SaleOffers.Queries.GetSaleOffer
{
    public sealed record GetSaleOfferQuery(int Id) : IQuery<GetSaleOfferResponse?>;
}

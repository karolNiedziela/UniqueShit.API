using UniqueShit.Application.Core.Messaging;

namespace UniqueShit.Application.Features.Offers.Queries.GetOffer
{
    public sealed record GetOfferQuery(int Id) : IQuery<GetOfferResponse?>;
}

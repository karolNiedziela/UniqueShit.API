using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Features.Offers.Contracts.Responses;

namespace UniqueShit.Application.Features.Offers.Queries.GetOffer
{
    public sealed record GetOfferQuery(int Id) : IQuery<GetOfferResponse?>;
}

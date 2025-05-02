using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Queries;

namespace UniqueShit.Application.Features.Offers.Queries.MyOffers
{
    public sealed record MyOffersQuery : IQuery<PagedList<MyOffersResponse>>;
}

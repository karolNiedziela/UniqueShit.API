using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Queries;

namespace UniqueShit.Application.Features.Offers.SaleOffers.Queries.MySaleOffers
{
    public sealed record MySaleOffersQuery : IQuery<PagedList<MySaleOffersResponse>>;
}

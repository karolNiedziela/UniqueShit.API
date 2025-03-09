using UniqueShit.Application.Core.Messaging;

namespace UniqueShit.Application.Features.Offers.Queries.GetFilters
{
    public sealed record GetOfferFiltersQuery() : IQuery<OfferFiltersResponse>;
}

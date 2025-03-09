using UniqueShit.Application.Core.Messaging;

namespace UniqueShit.Application.Features.OfferFilters.GetFilters
{
    public sealed record GetOfferFiltersQuery() : IQuery<OfferFiltersResponse>;
}

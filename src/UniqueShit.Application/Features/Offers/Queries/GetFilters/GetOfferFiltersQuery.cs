using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Features.Offers.Contracts.Responses;

namespace UniqueShit.Application.Features.Offers.Queries.GetFilters
{
    public sealed record GetOfferFiltersQuery() : IQuery<OfferFiltersResponse>;
}

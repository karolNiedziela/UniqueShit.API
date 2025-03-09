using UniqueShit.Application.Core.Messaging;

namespace UniqueShit.Application.Features.OfferFilters.GetSizesFilter
{
    public sealed record GetSizesFilterQuery(int ProductCategoryId) : IQuery<SizesFiltersResponse>;
}

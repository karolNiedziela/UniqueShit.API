using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Queries;

namespace UniqueShit.Application.Features.Offers.Queries.GetOffers
{
    public sealed record GetOffersQuery(
        int OfferTypeId,
        int PageNumber,
        int PageSize,
        int? ItemConditionId = null,
        int? SizeId = null,
        int? ManufacturerId = null,
        int? ProductCategoryId = null) : IQuery<PagedList<GetOffersResponse>>;
}

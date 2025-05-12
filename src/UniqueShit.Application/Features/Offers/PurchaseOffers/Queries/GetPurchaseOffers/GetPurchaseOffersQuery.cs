using Microsoft.AspNetCore.Mvc;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Queries;

namespace UniqueShit.Application.Features.Offers.PurchaseOffers.Queries.GetPurchaseOffers
{
    public sealed record GetPurchaseOffersQuery(
        [FromQuery(Name = "pn")] int PageNumber,
        [FromQuery(Name = "ps")] int PageSize,
        [FromQuery(Name = "mid")] int? ModelId = null,
        [FromQuery(Name = "bid")] int? BrandId = null,
        [FromQuery(Name = "pcid")] int? ProductCategoryId = null) : IQuery<PagedList<GetPurchaseOffersResponse>>;
}

﻿using Microsoft.AspNetCore.Mvc;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Queries;

namespace UniqueShit.Application.Features.Offers.Queries.GetOffers
{
    public sealed record GetOffersQuery(
        [FromQuery(Name = "otid")]int OfferTypeId,
        [FromQuery(Name = "pn")] int PageNumber,
        [FromQuery(Name = "ps")] int PageSize,
        [FromQuery(Name = "minp")] decimal? MinimalPrice,
        [FromQuery(Name = "maxp")] decimal? MaximumPrice,
        [FromQuery(Name = "icid")] int? ItemConditionId = null,
        [FromQuery(Name = "mid")] int? ModelId = null,
        [FromQuery(Name = "sid")] int? SizeId = null,
        [FromQuery(Name = "bid")] int? BrandId = null,
        [FromQuery(Name = "pcid")] int? ProductCategoryId = null) : IQuery<PagedList<GetOffersResponse>>;
}

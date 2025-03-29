using Microsoft.AspNetCore.Mvc;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Queries;

namespace UniqueShit.Application.Features.Models.Queries.GetModels
{
    public sealed record GetModelsQuery
        (
        [FromQuery]int? ProductCategoryId,
        [FromQuery]int? BrandId,
        [FromQuery]string? SearchTerm
        ): IQuery<PagedList<GetModelsResponse>>;
}

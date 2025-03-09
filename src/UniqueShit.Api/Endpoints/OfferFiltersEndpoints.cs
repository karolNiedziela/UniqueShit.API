using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniqueShit.Application.Features.OfferFilters.GetFilters;
using UniqueShit.Application.Features.OfferFilters.GetSizesFilter;

namespace UniqueShit.Api.Endpoints
{
    internal static class OfferFiltersEndpoints
    {
        public static void AddOfferFiltersEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("offers-filters");

            group.MapGet("", async (ISender sender) =>
            {
                var result = await sender.Send(new GetOfferFiltersQuery());

                return Results.Ok(result);
            });

            group.MapGet("{productCategoryId:int}/sizes", async ([FromRoute]int productCategoryId, ISender sender) =>
            {
                var result = await sender.Send(new GetSizesFilterQuery(productCategoryId));
                return Results.Ok(result);
            });
        }
    }
}

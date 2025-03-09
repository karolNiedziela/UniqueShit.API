using MediatR;
using UniqueShit.Application.Features.Offers.CreateOffer;

namespace UniqueShit.Api.Endpoints
{
    internal static class OfferEndpoints
    {
        public static void AddOfferEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("offers");

            group.MapPost("", async (CreateOfferCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.Created($"/offers/{result}", result);
            });
        }
    }
}

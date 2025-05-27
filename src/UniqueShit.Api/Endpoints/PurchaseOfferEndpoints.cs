using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Application.Features.Offers.PurchaseOffers.CreatePurchaseOffer;
using UniqueShit.Application.Features.Offers.PurchaseOffers.Queries.GetPurchaseOffers;
using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class PurchaseOfferEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("purchase-offers");

            group.MapGet("", GetPurchaseOffers)
               .WithName(nameof(GetPurchaseOffers))
               .WithMetadata(
                new SwaggerOperationAttribute(summary: "Get purchase offers"),
                new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            group.MapPost("", CreatePurchaseOffer)
                .WithName(nameof(CreatePurchaseOffer))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Add purchase offer"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status201Created),
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest))
                .RequireAuthorization();

            return builder;
        }

        public static async Task<Ok<PagedList<GetPurchaseOffersResponse>>> GetPurchaseOffers(
            [AsParameters] GetPurchaseOffersQuery query,
            ISender sender)
        {
            var offers = await sender.Send(query);

            return TypedResults.Ok(offers);
        }

        public static async Task<Results<Ok, BadRequest<List<Error>>>> CreatePurchaseOffer(
            CreatePurchaseOfferCommand command,
            ISender sender)
        {
            var result = await sender.Send(command);

            return result.Match<Results<Ok, BadRequest<List<Error>>>>(
                 onSuccess: (int purchaseOfferId) =>
                 {                 
                     return TypedResults.Ok();
                 },
                onError: (List<Error> errors) => TypedResults.BadRequest(errors));
        }
    }
}

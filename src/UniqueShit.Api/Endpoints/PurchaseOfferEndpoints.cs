using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Features.Offers.PurchaseOffers.CreatePurchaseOffer;
using UniqueShit.Application.Features.Offers.PurchaseOffers.Queries.GetPurchaseOffer;
using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class PurchaseOfferEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("purchase-offers");

            group.MapGet("{id:int}", GetPurchaseOffer)
                .WithName(nameof(GetPurchaseOffer))
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Get sale offer"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
                 new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));

            group.MapPost("", CreatePurchaseOffer)
                .WithName(nameof(CreatePurchaseOffer))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Add purchase offer"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status201Created),
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest))
                .RequireAuthorization();

            return builder;
        }

        public static async Task<Results<Ok<GetPurchaseOfferResponse>, NotFound>> GetPurchaseOffer(int id, ISender sender)
        {
            var purchaseOffer = await sender.Send(new GetPurchaseOfferQuery(id));
            if (purchaseOffer is null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(purchaseOffer);
        }

        public static async Task<Results<Created, BadRequest<List<Error>>>> CreatePurchaseOffer(
            CreatePurchaseOfferCommand command,
            ISender sender,
            LinkGenerator linkGenerator,
            HttpContext context)
        {
            var result = await sender.Send(command);

            return result.Match<Results<Created, BadRequest<List<Error>>>>(
                 onSuccess: (int purchaseOfferId) =>
                 {
                     var offerLink = linkGenerator.GetUriByName(context,
                           endpointName: nameof(GetPurchaseOffer),
                           values: new { id = result.Value }
                           );

                     return TypedResults.Created(new Uri(offerLink!));
                 },
                onError: (List<Error> errors) => TypedResults.BadRequest(errors));
        }
    }
}

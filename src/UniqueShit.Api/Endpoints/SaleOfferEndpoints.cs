using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Application.Features.Offers.SaleOffers.Commands.CreateSaleOffer;
using UniqueShit.Application.Features.Offers.SaleOffers.Commands.DeleteSaleOffer;
using UniqueShit.Application.Features.Offers.SaleOffers.Queries.GetSaleOffer;
using UniqueShit.Application.Features.Offers.SaleOffers.Queries.GetSaleOffers;
using UniqueShit.Application.Features.Offers.SaleOffers.Queries.MySaleOffers;
using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class SaleOfferEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("sale-offers");

            group.MapGet("{offerId:int}", GetSaleOffer)
                .WithName(nameof(GetSaleOffer))
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Get sale offer"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
                 new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));

            group.MapGet("", GetSaleOffers)
                .WithName(nameof(GetSaleOffers))
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Get sale offers"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            group.MapGet("my", GetMySaleOffers)
                .WithName(nameof(GetMySaleOffers))
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Get my sale offers"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK))
                .RequireAuthorization();

            group.MapPost("", CreateSaleOffer)
                .WithName(nameof(CreateSaleOffer))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Add sale offer"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status201Created),
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest))
                .RequireAuthorization();

            group.MapDelete("{offerId:int}", DeleteSaleOffer)
                .WithName(nameof(DeleteSaleOffer))
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Delete sale offer"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status204NoContent),
                 new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest),
                 new ProducesResponseTypeAttribute(StatusCodes.Status403Forbidden))
                .RequireAuthorization();

            return builder;
        }

        public static async Task<Results<Ok<GetSaleOfferResponse>, NotFound>> GetSaleOffer(int offerId, ISender sender, HttpContext context)
        {
            var offer = await sender.Send(new GetSaleOfferQuery(offerId));
            if (offer is null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(offer);
        }

        public static async Task<Ok<PagedList<GetSaleOffersResponse>>> GetSaleOffers(
         [AsParameters] GetSaleOffersQuery query,
         ISender sender)
        {
            var offers = await sender.Send(query);

            return TypedResults.Ok(offers);
        }

        public static async Task<Ok<PagedList<MySaleOffersResponse>>> GetMySaleOffers(
         ISender sender)
        {
            var myOffers = await sender.Send(new MySaleOffersQuery());

            return TypedResults.Ok(myOffers);
        }

        public static async Task<Results<Created, BadRequest<List<Error>>>> CreateSaleOffer(
            CreateSaleOfferCommand command,
            ISender sender,
            LinkGenerator linkGenerator,
            HttpContext context)
        {
            var result = await sender.Send(command);

            return result.Match<Results<Created, BadRequest<List<Error>>>>(
                 onSuccess: (int offerId) =>
                 {
                     var offerLink = linkGenerator.GetUriByName(context,
                           endpointName: nameof(GetSaleOffer),
                           values: new { offerId = result.Value }
                           );

                     return TypedResults.Created(new Uri(offerLink!));
                 },
                onError: (List<Error> errors) => TypedResults.BadRequest(errors));
        }

        public static async Task<Results<NoContent, BadRequest<List<Error>>, ForbidHttpResult>> DeleteSaleOffer(
            int offerId,
            ISender sender)
        {
            var result = await sender.Send(new DeleteSaleOfferCommand(offerId));

            if (result.IsSuccess)
            {
                return TypedResults.NoContent();
            }

            if (result.Errors.Any(x => x.ErrorType == ErrorType.Forbidden))
            {
                return TypedResults.Forbid();
            }

            return TypedResults.BadRequest(result.Errors);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Application.Features.FavouriteOffers.Commands.AddFavouriteOffer;
using UniqueShit.Application.Features.FavouriteOffers.Commands.RemoveFavouriteOffer;
using UniqueShit.Application.Features.FavouriteOffers.Queries.GetFavouriteOffers;
using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class FavouriteOfferEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("favourite-offers");

            group.MapGet("", GetFavouriteOffers)
                 .WithName(nameof(GetFavouriteOffers))
                 .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Get favourite offers"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK))
                 .RequireAuthorization();

            group.MapPost("", AddFavouriteOffer)
                .WithName(nameof(AddFavouriteOffer))
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Add favourite offer"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
                 new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest))
                .RequireAuthorization();

            group.MapDelete("{favouriteOfferId:int}", DeleteFavouriteOffer)
                .WithName(nameof(DeleteFavouriteOffer))
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Delete offer"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status204NoContent),
                 new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest))
                .RequireAuthorization();

            return builder;
        }

        public static async Task<Ok<PagedList<GetFavouriteOffersResponse>>> GetFavouriteOffers(
            [AsParameters] GetFavouriteOffersQuery query,
            ISender sender)
        {
            var favouriteOffers = await sender.Send(query);

            return TypedResults.Ok(favouriteOffers);
        }

        public static async Task<Results<Ok, BadRequest<List<Error>>>> AddFavouriteOffer(
            AddFavouriteOfferCommand command,
            ISender sender)
        {
            var result = await sender.Send(command);
            if (result.IsFailure)
            {
                return TypedResults.BadRequest(result.Errors);
            }

            return TypedResults.Ok();
        }

        public static async Task<Results<NoContent, BadRequest<List<Error>>>> DeleteFavouriteOffer(
         int favouriteOfferId,
         ISender sender)
        {
            var result = await sender.Send(new RemoveFavouriteOfferCommand(favouriteOfferId));
            if (result.IsSuccess)
            {
                return TypedResults.NoContent();
            }

            return TypedResults.BadRequest(result.Errors);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Application.Features.Offers.Commands.CreateOffer;
using UniqueShit.Application.Features.Offers.Queries.GetFilters;
using UniqueShit.Application.Features.Offers.Queries.GetOffers;
using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class OfferEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("offers");

            group.MapGet("{offerId:int}", GetOffer)
                .WithName(nameof(GetOffer))
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Get offer"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
                 new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));

            group.MapGet("", GetOffers)
                .WithName(nameof(GetOffers))
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Get offers"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            group.MapGet("filters", GetOfferFilters)
                .WithName(nameof(GetOfferFilters))
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Get offer filters"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            group.MapPost("", CreateOffer)
                .WithName(nameof(CreateOffer))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Add offer"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status201Created),
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));

            return builder;
        }

        public static async Task<Results<Ok, NotFound>> GetOffer(Guid offerId, ISender sender, HttpContext context)
        {
            return TypedResults.Ok();
        }

        public static async Task<Ok<PagedList<GetOffersResponse>>> GetOffers(
            int offerTypeId,
            int pageNumber,
            int pageSize,
            int? itemConditionId,
            int? sizeId,
            int? manufacturerId,
            int? productCategoryId,
            ISender sender)
        {
            var offers = await sender.Send(new GetOffersQuery(offerTypeId, pageNumber, pageSize, itemConditionId, sizeId, manufacturerId, productCategoryId));

            return TypedResults.Ok(offers);
        }

        public static async Task<Ok<OfferFiltersResponse>> GetOfferFilters(ISender sender)
        {
            var result = await sender.Send(new GetOfferFiltersQuery());

            return TypedResults.Ok(result);
        }

        public static async Task<Results<Created, BadRequest<List<Error>>>> CreateOffer(
            CreateOfferCommand command,
            ISender sender,
            LinkGenerator linkGenerator,
            HttpContext context)
        {
            var result = await sender.Send(command);

            return result.Match<Results<Created, BadRequest<List<Error>>>>(
                 onSuccess: (int offerId) =>
                 {
                     var offerLink = linkGenerator.GetUriByName(context,
                           endpointName: nameof(GetOffer),
                           values: new { offerId = result.Value }
                           );

                     return TypedResults.Created(new Uri(offerLink!));
                 },
                onError: (List<Error> errors) => TypedResults.BadRequest(errors));
        }
    }
}

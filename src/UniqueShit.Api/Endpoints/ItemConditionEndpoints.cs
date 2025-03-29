using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Application.Features.ItemConditions.Queries;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class ItemConditionEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("item-conditions");

            group.MapGet("", GetItemConditions)
                .WithName(nameof(GetItemConditions))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Get item conditions"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            return builder;
        }

        public static async Task<Ok<List<EnumerationResponse>>> GetItemConditions(
           ISender sender)
        {
            var query = new GetItemConditionsQuery();

            var itemConditions = await sender.Send(query);

            return TypedResults.Ok(itemConditions);
        }
    }
}

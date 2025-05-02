using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Domain.Enumerations;

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

        public static Task<Ok<List<EnumerationResponse>>> GetItemConditions(
           ISender sender)
        {
            List<EnumerationResponse>? itemConditions = ItemCondition.List
                .Select(x => new EnumerationResponse(x.Id, x.Name))
                .ToList();

            return Task.FromResult(TypedResults.Ok(itemConditions));
        }
    }
}

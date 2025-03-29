using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Application.Features.Colours.Queries;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class ColourEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("colours");

            group.MapGet("", GetColours)
                .WithName(nameof(GetColours))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Get colours"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            return builder;
        }

        public static async Task<Ok<List<EnumerationResponse>>> GetColours(
           ISender sender)
        {
            var query = new GetColorsQuery();

            var colours = await sender.Send(query);

            return TypedResults.Ok(colours);
        }
    }
}

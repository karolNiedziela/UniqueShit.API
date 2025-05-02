using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Domain.Enumerations;

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

        public static Task<Ok<List<EnumerationResponse>>> GetColours(
           ISender sender)
        {
            var colours = Colour.List
                .Select(x => new EnumerationResponse(x.Id, x.Name))
                .ToList();

            return Task.FromResult(TypedResults.Ok(colours));
        }
    }
}

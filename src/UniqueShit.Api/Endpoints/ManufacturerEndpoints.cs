using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Application.Features.Manufacturers.Queries;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class ManufacturerEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("manufacturers");

            group.MapGet("", GetManufacturers)
                .WithName(nameof(GetManufacturers))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Get manufacturers"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            return builder;
        }

        public static async Task<Ok<PagedList<GetManufacturersResponse>>> GetManufacturers(
            string? searchTerm,
            ISender sender)
        {
            var query = new GetManufacturersQuery(searchTerm);

            var models = await sender.Send(query);

            return TypedResults.Ok(models);
        }
    }
}

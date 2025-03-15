using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Features.Sizes.Queries.GetSizes;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class SizeEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("sizes");

            group.MapGet("", GetSizes)
                .WithName(nameof(GetSizes))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Get sizes"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status200OK))
                .RequireAuthorization();

            return builder;
        }

        public static async Task<Ok<List<GetSizesResponse>>> GetSizes(
            int productCategoryId, 
            ISender sender)
        {
            var query = new GetSizesQuery(productCategoryId);

            var sizes = await sender.Send(query);

            return TypedResults.Ok(sizes);
        }
    }
}

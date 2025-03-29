using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Application.Features.Brands.Queries;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class BrandEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("brands");

            group.MapGet("", GetBrands)
                .WithName(nameof(GetBrands))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Get brands"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            return builder;
        }

        public static async Task<Ok<List<GetBrandsResponse>>> GetBrands(
            string? searchTerm,
            ISender sender)
        {
            var query = new GetBrandsQuery(searchTerm);

            var brands = await sender.Send(query);

            return TypedResults.Ok(brands);
        }
    }
}

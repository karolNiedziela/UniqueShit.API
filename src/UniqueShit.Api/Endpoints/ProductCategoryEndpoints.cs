using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Domain.Enumerations;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class ProductCategoryEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("product-categories");
            group.MapGet("", GetProductCategories)
                .WithName(nameof(GetProductCategories))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Get product categories"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
            return builder;
        }
        public static Task<Ok<List<EnumerationResponse>>> GetProductCategories(
            ISender sender)
        {
            var productCategories =
                ProductCategory.List.Select(x => new EnumerationResponse(x.Id, x.Name))
                .OrderBy(x => x.Name)
                .ToList();

            return Task.FromResult(TypedResults.Ok(productCategories));
        }
    }
}

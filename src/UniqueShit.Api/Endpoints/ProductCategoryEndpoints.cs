
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Application.Features.ProductCategories.Queries;

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
        public static async Task<Ok<List<EnumerationResponse>>> GetProductCategories(
            ISender sender)
        {
            var productCategories = await sender.Send(new GetProductCategoriesQuery());
            return TypedResults.Ok(productCategories);
        }
    }
}

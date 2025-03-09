using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Application.Features.Models.Queries.GetModels;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class ModelEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("models");

            group.MapGet("", GetModels)
                .WithName(nameof(GetModels))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Get model"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            return builder;
        }

        public static async Task<Ok<PagedList<GetModelsResponse>>> GetModels(int productCategoryId,
            int manufacturerId,
            string? searchTerm,
            ISender sender)
        {
            var query = new GetModelsQuery(productCategoryId, manufacturerId, searchTerm);

            var models = await sender.Send(query);

            return TypedResults.Ok(models);
        }
    }
}

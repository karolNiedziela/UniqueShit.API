using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Features.Models.Commands;
using UniqueShit.Application.Features.Models.Queries.GetModels;
using UniqueShit.Domain.Core.Primitives;

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
                    new SwaggerOperationAttribute(summary: "Get models"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            group.MapPost("", CreateModel)
                .WithName(nameof(CreateModel))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Create model"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest))
                .RequireAuthorization();

            return builder;
        }

        public static async Task<Ok<List<GetModelsResponse>>> GetModels(
            [AsParameters] GetModelsQuery query,
            ISender sender)
        {
            var models = await sender.Send(query);

            return TypedResults.Ok(models);
        }

        public static async Task<Results<Ok<CreateModelResponse>, BadRequest<List<Error>>>> CreateModel(
            CreateModelCommand command,
            ISender sender)
        {
            var result = await sender.Send(command);

            return result.Match<Results<Ok<CreateModelResponse>, BadRequest<List<Error>>>>(
               onSuccess: (CreateModelResponse createModelResponse) =>
               {
                   return TypedResults.Ok(result.Value);
               },
              onError: (List<Error> errors) => TypedResults.BadRequest(errors));
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Features.AppUsers.Commands;
using UniqueShit.Infrastructure.Authentication;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class AppUserEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("app-users");

            group.MapPost("", CreateUser)
                .WithName(nameof(CreateUser))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "ADB2C create user connector"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status200OK))
                .AddEndpointFilter<CreateAppUserEndpointFilter>();

            return builder;
        }

        public static async Task<Ok> CreateUser(CreateAppUserCommand command, ISender sender)
        {
            await sender.Send(command);

            return TypedResults.Ok();
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Features.AppUsers.Commands.CreateAppUser;
using UniqueShit.Application.Features.AppUsers.Commands.UpdateAppUser;
using UniqueShit.Application.Features.AppUsers.Queries.GetUser;
using UniqueShit.Infrastructure.Authentication;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class AppUserEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("app-users");

            group.MapGet("{userId:guid}", GetUser)
              .WithName(nameof(GetUser))
              .WithMetadata(
               new SwaggerOperationAttribute(summary: "Get user information"),
               new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
               new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));

            group.MapPost("", CreateUser)
                .WithName(nameof(CreateUser))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "ADB2C create user connector"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status200OK))
                .AddEndpointFilter<CreateAppUserEndpointFilter>();

            group.MapPatch("", UpdateUser)
                .WithName(nameof(UpdateUser))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Update user information"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status204NoContent),
                    new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound))
            .RequireAuthorization();

            return builder;
        }

        public static async Task<Results<Ok<GetUserResponse>, NotFound>> GetUser(Guid userId, ISender sender)
        {
            var user = await sender.Send(new GetUserQuery(userId));
            if (user is null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(user);
        }

        public static async Task<Ok> CreateUser(CreateAppUserCommand command, ISender sender)
        {
            await sender.Send(command);

            return TypedResults.Ok();
        }

        public static async Task<Results<NoContent, NotFound>> UpdateUser(UpdateAppUserCommand command, ISender sender)
        {
            var result = await sender.Send(command);
            if (result.IsFailure)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.NoContent();
        }
    }
}

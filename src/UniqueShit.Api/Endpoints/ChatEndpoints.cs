
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Application.Features.Chats;
using UniqueShit.Application.Features.Chats.Commands.SendMessage;
using UniqueShit.Application.Features.Chats.Queries.GetChat;
using UniqueShit.Application.Features.Chats.Queries.GetChatPreviews;
using UniqueShit.Application.Features.Chats.Queries.GetOrCreateChat;
using UniqueShit.Chatting;
using UniqueShit.Infrastructure.Realtime.Hubs;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class ChatEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/private-chats")
                .WithTags("Private chats")
                .RequireAuthorization();

            group.MapGet("{id:guid}/messages", GetChat)
                .WithName(nameof(GetChat))
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Get chat"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            group.MapGet("previews", GetChatPreviews)
               .WithName(nameof(GetChatPreviews))
               .WithMetadata(
                new SwaggerOperationAttribute(summary: "Get chat previews"),
                new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            group.MapPost("get-or-create", GetOrCreate)
                .WithName(nameof(GetOrCreate))
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Get or create chat"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            group.MapPost("send-message", SendMessage)
                .WithName(nameof(SendMessage))
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Send chat message"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            return builder;
        }

        public static async Task<Ok<ChatResponse?>> GetChat(Guid id, [FromQuery(Name = "pn")] int pageNumber, ISender sender)
        {
            var chatResponse = await sender.Send(new GetChatQuery(id, pageNumber));

            return TypedResults.Ok<ChatResponse?>(chatResponse);
        }

        public static async Task<Ok<PagedList<GetChatPreviewsResponse>>> GetChatPreviews([FromQuery(Name = "pn")] int pageNumber, ISender sender)
        {
            var chatPreviews = await sender.Send(new GetChatPreviewsQuery(pageNumber));

            return TypedResults.Ok(chatPreviews);
        }

        public static async Task<Ok<ChatResponse>> GetOrCreate(GetOrCreateChatQuery query, ISender sender)
        {
            var chatResponse = await sender.Send(query);

            return TypedResults.Ok(chatResponse);
        }

        public static async Task<Ok<SendMessageResponse>> SendMessage(SendMessageCommand command, ISender sender, IHubContext<ChatHub, IChatClient> hubContext)
        {
            var result = await sender.Send(command);

            await hubContext.Clients.User(result.Value.ReceiverId.ToString()).NewChatMessage(result.Value);

            return TypedResults.Ok(result.Value);
        }
    }
}

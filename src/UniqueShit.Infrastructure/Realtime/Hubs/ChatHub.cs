using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using UniqueShit.Application.Core.Authentication;
using UniqueShit.Chatting.Models;
using UniqueShit.Domain.Chatting.Repositories;

namespace UniqueShit.Infrastructure.Realtime.Hubs
{
    [Authorize]
    public sealed class ChatHub : Hub<IChatClient>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly ISender _sender;

        public ChatHub(IChatMessageRepository chatMessageRepository, ISender sender)
        {
            _chatMessageRepository = chatMessageRepository;
            _sender = sender;
        }

        private static readonly ConcurrentDictionary<Guid, OnlineUserDto> _onlineUsers = new();

        public override async Task OnConnectedAsync()
        {
            var userId = GetUserId();
            var displayName = Context.User!.FindFirst(AzureADClaimTypes.DisplayName)!.Value;

            if (_onlineUsers.TryGetValue(userId, out var onlineUser))
            {
                onlineUser.ConnectionId = Context.ConnectionId;
                await base.OnConnectedAsync();
                return;
            }

            var onlineUserDto = new OnlineUserDto
            {
                Id = userId,
                ConnectionId = Context.ConnectionId,
                DisplayName = displayName
            };

            _onlineUsers.TryAdd(userId, onlineUserDto);

            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _onlineUsers.TryRemove(GetUserId(), out _);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task<IEnumerable<ChatPreviewDto>> GetUserChats()
        {
            //var chatPreviews = await _chatMessageRepository.GetUserChatPreviewsAsync(GetUserId());

            //return chatPreviews;

            return [];
        }

        private Guid GetUserId() => Guid.Parse(Context.UserIdentifier!);
    }
}

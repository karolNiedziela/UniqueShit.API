using UniqueShit.Application.Features.Chats.Commands.SendMessage;

namespace UniqueShit.Infrastructure.Realtime.Hubs
{
    public interface IChatClient
    {
        Task NewChatMessage(SendMessageResponse message);
    }
}

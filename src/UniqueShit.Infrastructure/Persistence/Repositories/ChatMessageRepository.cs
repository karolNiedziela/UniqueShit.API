using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Chatting;
using UniqueShit.Chatting.Models;
using UniqueShit.Domain.Chatting.Repositories;

namespace UniqueShit.Infrastructure.Persistence.Repositories
{
    internal sealed class ChatMessageRepository : IChatMessageRepository
    {
        private readonly DbSet<ChatMessage> _chatMessages;

        public ChatMessageRepository(IDbContext context)
        {
            _chatMessages = context.Set<ChatMessage>();
        }

        public void Add(ChatMessage chatMessage)
            => _chatMessages.Add(chatMessage);

        public async Task<List<ChatMessage>> GetChatMessageAsync(Guid senderId, Guid receiverId)
        {
            //var chatMessages = await _chatMessages
            //    .Include(m => m.Sender)
            //    .Include(m => m.Receiver)
            //    .Where(x => (x.SenderId == senderId && x.ReceiverId == receiverId) ||
            //                 (x.SenderId == receiverId && x.ReceiverId == senderId))
            //    .ToListAsync();

            //return chatMessages;

            return [];
        }

        public async Task<IEnumerable<ChatPreviewDto>> GetUserChatPreviewsAsync(Guid userId)
        {
            //var chatPreviews = await _chatMessages
            //    .Include(m => m.Sender)
            //    .Include(m => m.Receiver)
            //    .Where(m => m.ReceiverId == userId || m.SenderId == userId)
            //    .GroupBy(x =>  x.SenderId)
            //    .Select(g => g
            //        .OrderByDescending(x => x.SentAt)
            //        .Select(x => new ChatPreviewDto
            //        {
            //            UserId = x.SenderId == userId ? x.Receiver.ADObjectId : x.Sender.ADObjectId,
            //            DisplayName = x.SenderId == userId ? x.Receiver.DisplayName : x.Sender.DisplayName,
            //            LastMessage = x.Content,
            //            SentAt = x.SentAt,
            //            IsRead = x.IsRead
            //        })
            //    )
            //    .ToListAsync();

            //return chatPreviews;

            return [];
        }
    }
}

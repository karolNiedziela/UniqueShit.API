using Microsoft.EntityFrameworkCore;
using System.Linq;
using UniqueShit.Application.Core.Authentication;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Domain.Chatting;

namespace UniqueShit.Application.Features.Chats.Queries.GetChat
{
    public sealed class GetChatQueryHandler : IQueryHandler<GetChatQuery, ChatResponse?>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        public GetChatQueryHandler(IDbContext dbContext, IUserIdentifierProvider userIdentifierProvider)
        {
            _dbContext = dbContext;
            _userIdentifierProvider = userIdentifierProvider;
        }

        public async Task<ChatResponse?> Handle(GetChatQuery request, CancellationToken cancellationToken)
        {
            var chat = await _dbContext.Set<PrivateChat>()
                .AsNoTracking()
                .Include(c => c.Messages)
                .Where(x => x.Id == request.ChatId &&
                            (x.User1Id == _userIdentifierProvider.UserId || x.User2Id == _userIdentifierProvider.UserId))
                .Select(x => new ChatResponse(
                    x.Id,
                    x.User1Id == _userIdentifierProvider.UserId ? x.User2.DisplayName : x.User1.DisplayName,
                    x.Messages
                    .Take(PagedBase.DefaultPageSize)
                    .OrderBy(x => x.SentAt)
                    .Select(m => new ChatMessageResponse(
                        m.Id,
                        m.SenderId,
                        m.Content,
                        m.SentAt,
                        m.IsRead)).ToList())
                ).FirstOrDefaultAsync(cancellationToken);

            return chat;
        }
    }
}

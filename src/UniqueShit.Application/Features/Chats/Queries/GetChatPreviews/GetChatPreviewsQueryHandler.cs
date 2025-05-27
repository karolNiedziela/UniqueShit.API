using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Authentication;
using UniqueShit.Application.Core.Extensions;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Domain.Chatting;

namespace UniqueShit.Application.Features.Chats.Queries.GetChatPreviews
{
    public sealed class GetChatPreviewsQueryHandler : IQueryHandler<GetChatPreviewsQuery, PagedList<GetChatPreviewsResponse>>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        public GetChatPreviewsQueryHandler(IDbContext dbContext, IUserIdentifierProvider userIdentifierProvider)
        {
            _dbContext = dbContext;
            _userIdentifierProvider = userIdentifierProvider;
        }

        public async Task<PagedList<GetChatPreviewsResponse>> Handle(GetChatPreviewsQuery request, CancellationToken cancellationToken)
        {
            var chatPreviews = await _dbContext.Set<PrivateChat>()
                .AsNoTracking()
                .Where(x => x.User1Id == _userIdentifierProvider.UserId || x.User2Id == _userIdentifierProvider.UserId)
                .Select(chat => new
                {
                    chat.Id,
                    LatestMessage = chat.Messages
                        .Select(x => new
                        {
                            x.Content,
                            x.IsRead,
                            x.SentAt
                        })
                        .OrderByDescending(m => m.SentAt)
                        .First(),
                    DisplayName = chat.User1Id == _userIdentifierProvider.UserId ? chat.User2.DisplayName : chat.User1.DisplayName
                })
                .OrderByDescending(x => x.LatestMessage.SentAt)
                .Select(x => new GetChatPreviewsResponse(
                    x.Id,
                    x.DisplayName,
                    x.LatestMessage.Content,
                    x.LatestMessage.SentAt,
                    x.LatestMessage.IsRead))
                .PaginateAsync(request.PageNumber, PagedBase.DefaultPageSize, cancellationToken);

            return chatPreviews;
        }
    }
}

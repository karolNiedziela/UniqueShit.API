using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Authentication;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Chatting;
using UniqueShit.Domain.Chatting;
using UniqueShit.Domain.Chatting.Repositories;

namespace UniqueShit.Application.Features.Chats.Queries.GetOrCreateChat
{
    public sealed class GetOrCreateChatQueryHandler : IQueryHandler<GetOrCreateChatQuery, ChatResponse>
    {
        private readonly Guid _userId;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbContext _dbContext;
        private readonly IPrivateChatRepository _privateChatRepository;

        public GetOrCreateChatQueryHandler(IUserIdentifierProvider userIdentifierProvider, IUnitOfWork unitOfWork, IDbContext dbContext, IPrivateChatRepository privateChatRepository)
        {
            _userId = userIdentifierProvider.UserId;
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _privateChatRepository = privateChatRepository;
        }

        public async Task<ChatResponse> Handle(GetOrCreateChatQuery request, CancellationToken cancellationToken)
        {
            var existingPrivateChat = await _privateChatRepository.GetByUserIdsAsync(request.ReceiverId, _userId);
            if (existingPrivateChat is null)
            {
                var privateChat = PrivateChat.Create(Guid.CreateVersion7(), _userId, request.ReceiverId);
                _privateChatRepository.Add(privateChat);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ChatResponse(privateChat.Id, request.DisplayName, []);
            }

            var messages = await _dbContext.Set<ChatMessage>()
                .AsNoTracking()
                .Where(x => x.PrivateChatId == existingPrivateChat.Id)
                .Take(PagedBase.DefaultPageSize)
                .OrderBy(x => x.SentAt)
                .Select(x => new ChatMessageResponse(
                    x.Id,
                    x.SenderId,
                    x.Content,
                    x.SentAt,
                    x.IsRead))
                .ToListAsync(cancellationToken);

            return new ChatResponse(existingPrivateChat.Id, request.DisplayName, messages);
        }
    }
}

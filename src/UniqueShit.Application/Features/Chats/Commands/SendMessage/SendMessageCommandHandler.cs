using UniqueShit.Application.Core.Authentication;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Chatting;
using UniqueShit.Domain.Chatting.Repositories;
using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Application.Features.Chats.Commands.SendMessage
{
    public sealed class SendMessageCommandHandler : ICommandHandler<SendMessageCommand, Result<SendMessageResponse>>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IPrivateChatRepository _privateChatRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TimeProvider _timeProvider;

        private readonly Guid _userId;

        public SendMessageCommandHandler(
            IUnitOfWork unitOfWork,
            IUserIdentifierProvider userIdentifierProvider,
            TimeProvider timeProvider,
            IChatMessageRepository chatMessageRepository,
            IPrivateChatRepository privateChatRepository)
        {
            _unitOfWork = unitOfWork;
            _userId = userIdentifierProvider.UserId;
            _timeProvider = timeProvider;
            _chatMessageRepository = chatMessageRepository;
            _privateChatRepository = privateChatRepository;
        }

        public async Task<Result<SendMessageResponse>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var chat = await _privateChatRepository.GetAsync(request.ChatId);
            if (chat is null)
            {
                return new Error("Chat.NotFound", "Chat not found");
            }

            var newChatMessage = new ChatMessage(Guid.CreateVersion7(), request.ChatId, _userId, request.Content, _timeProvider.GetUtcNow());
            _chatMessageRepository.Add(newChatMessage);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var receiverId = chat.User1Id == _userId ? chat.User2Id : chat.User1Id;

            return new SendMessageResponse(newChatMessage.Id, newChatMessage.PrivateChatId, _userId, receiverId, newChatMessage.Content, newChatMessage.SentAt, newChatMessage.IsRead);
        }
    }
}

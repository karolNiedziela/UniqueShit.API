using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Queries;

namespace UniqueShit.Application.Features.Chats.Queries.GetChatPreviews
{
    public sealed record GetChatPreviewsQuery(int PageNumber) : IQuery<PagedList<GetChatPreviewsResponse>>;
}

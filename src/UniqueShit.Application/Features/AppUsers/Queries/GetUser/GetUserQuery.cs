using UniqueShit.Application.Core.Messaging;

namespace UniqueShit.Application.Features.AppUsers.Queries.GetUser
{
    public sealed record GetUserQuery(Guid UserId) : IQuery<GetUserResponse>;
}

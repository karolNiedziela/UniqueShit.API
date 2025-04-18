using UniqueShit.Application.Core.Messaging;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Application.Features.AppUsers.Commands
{
    public sealed record CreateAppUserCommand(string Email, string DisplayName, Guid ObjectId) : ICommand<Result>;
}

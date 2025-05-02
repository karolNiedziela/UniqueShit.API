using UniqueShit.Application.Core.Messaging;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Application.Features.Models.Commands
{
    public sealed record CreateModelCommand(string Name, int ProductCategoryId, int BrandId) : ICommand<Result<CreateModelResponse>>;
}

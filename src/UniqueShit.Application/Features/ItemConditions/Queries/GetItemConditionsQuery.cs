using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Responses;

namespace UniqueShit.Application.Features.ItemConditions.Queries
{
    public sealed record GetItemConditionsQuery : IQuery<List<EnumerationResponse>>;
}

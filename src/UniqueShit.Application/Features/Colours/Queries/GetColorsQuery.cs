using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Responses;

namespace UniqueShit.Application.Features.Colours.Queries
{
    public sealed record GetColorsQuery : IQuery<List<EnumerationResponse>>;
}

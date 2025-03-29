using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Domain.Enumerations;

namespace UniqueShit.Application.Features.ItemConditions.Queries
{
    public sealed class GetItemConditionsQueryHandler : IQueryHandler<GetItemConditionsQuery, List<EnumerationResponse>>
    {
        public Task<List<EnumerationResponse>> Handle(GetItemConditionsQuery request, CancellationToken cancellationToken)
        {
            var colours = ItemCondition.List
                .Select(x => new EnumerationResponse(x.Id, x.Name))
                .ToList();

            return Task.FromResult(colours);
        }
    }
}

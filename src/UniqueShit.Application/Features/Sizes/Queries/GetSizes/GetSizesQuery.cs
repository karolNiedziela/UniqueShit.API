using UniqueShit.Application.Core.Messaging;

namespace UniqueShit.Application.Features.Sizes.Queries.GetSizes
{
    public sealed record GetSizesQuery(int ProductCategoryId) : IQuery<List<GetSizesResponse>>;
}

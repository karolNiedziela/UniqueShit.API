using UniqueShit.Application.Core.Messaging;

namespace UniqueShit.Application.Features.Brands.Queries
{
    public sealed record GetBrandsQuery(string? SearchTerm) : IQuery<List<GetBrandsResponse>>;
}

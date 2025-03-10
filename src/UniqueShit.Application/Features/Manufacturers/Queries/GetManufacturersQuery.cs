using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Queries;

namespace UniqueShit.Application.Features.Manufacturers.Queries
{
    public sealed record GetManufacturersQuery(string? SearchTerm) : IQuery<PagedList<GetManufacturersResponse>>;
}

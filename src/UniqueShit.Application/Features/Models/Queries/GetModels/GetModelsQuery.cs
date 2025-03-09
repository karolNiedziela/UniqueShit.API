using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Queries;

namespace UniqueShit.Application.Features.Models.Queries.GetModels
{
    public sealed record GetModelsQuery
        (
        int ProductCategoryId,
        int ManufacturerId,
        string? SearchTerm
        ): IQuery<PagedList<GetModelsResponse>>;
}

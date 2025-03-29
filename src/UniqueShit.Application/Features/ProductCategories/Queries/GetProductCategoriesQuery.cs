using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Responses;

namespace UniqueShit.Application.Features.ProductCategories.Queries
{
    public sealed record GetProductCategoriesQuery() : IQuery<List<EnumerationResponse>>;
}

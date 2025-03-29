using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Domain.Enumerations;

namespace UniqueShit.Application.Features.ProductCategories.Queries
{
    public sealed class GetProductCategoriesQueryHandler : IQueryHandler<GetProductCategoriesQuery, List<EnumerationResponse>>
    {
        public Task<List<EnumerationResponse>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            var productCategories = 
                ProductCategory.List.Select(x => new EnumerationResponse(x.Id, x.Name))
                .OrderBy(x => x.Name)
                .ToList();

            return Task.FromResult(productCategories);
        }
    }
}

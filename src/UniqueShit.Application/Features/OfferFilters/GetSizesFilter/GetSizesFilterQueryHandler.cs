using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Domain.Enitities;

namespace UniqueShit.Application.Features.OfferFilters.GetSizesFilter
{
    internal sealed class GetSizesFilterQueryHandler : IQueryHandler<GetSizesFilterQuery, SizesFiltersResponse>
    {
        private readonly IDbContext _dbContext;

        public GetSizesFilterQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SizesFiltersResponse> Handle(GetSizesFilterQuery request, CancellationToken cancellationToken)
        {
            var sizes = await _dbContext.Set<Size>()
                .AsNoTracking()
                .Where(x => x.ProductCategoryId == request.ProductCategoryId)
                .Select(x => new SizeFilterResponse(x.Id, x.Value))
                .ToListAsync(cancellationToken);

            return new SizesFiltersResponse
            {
                Sizes = sizes
            };
        }
    }
}

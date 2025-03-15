using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Enumerations;
using UniqueShit.Application.Features.Offers.Contracts.Responses;

namespace UniqueShit.Application.Features.Offers.Queries.GetFilters
{
    internal sealed class GetOffersFiltersQueryHandler : IQueryHandler<GetOfferFiltersQuery, OfferFiltersResponse>
    {
        private readonly IDbContext _dbContext;

        public GetOffersFiltersQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OfferFiltersResponse> Handle(GetOfferFiltersQuery request, CancellationToken cancellationToken)
        {
            var colours = await _dbContext.Set<Colour>().AsNoTracking().ToListAsync(cancellationToken);
            var productCategories = await _dbContext.Set<ProductCategory>().AsNoTracking().ToListAsync(cancellationToken);
            var itemConditions = await _dbContext.Set<ItemCondition>().AsNoTracking().ToListAsync(cancellationToken);
            var manufacturers = await _dbContext.Set<Manufacturer>().AsNoTracking().ToListAsync(cancellationToken);

            return new OfferFiltersResponse
            {
                Colours = colours,
                ProductCategories = productCategories,
                ItemConditions = itemConditions,
                Manufacturers = manufacturers
            };
        }
    }
}

using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Offers;
using UniqueShit.Domain.Offers.Enumerations;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Infrastructure.Persistence.Repositories
{
    internal sealed class SaleOfferRepository : ISaleOfferRepository
    {
        private readonly DbSet<SaleOffer> _saleOffers;

        public SaleOfferRepository(IDbContext dbContext)
        {
            _saleOffers = dbContext.Set<SaleOffer>();
        }

        public async Task<SaleOffer?> Get(int saleOfferId)
            => await _saleOffers.FirstOrDefaultAsync(x => x.Id == saleOfferId);

        public async Task<bool> ActiveExistsAsync(int saleOfferId)
            => await _saleOffers.AnyAsync(x => x.OfferStateId == OfferState.Active.Id && x.Id == saleOfferId);

        public void Add(SaleOffer saleOffer)
            => _saleOffers.Add(saleOffer);

        public void Update(SaleOffer saleOffer)
            => _saleOffers.Update(saleOffer);

    }
}

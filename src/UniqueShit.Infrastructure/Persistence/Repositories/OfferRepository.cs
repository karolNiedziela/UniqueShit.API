using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Offers;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Infrastructure.Persistence.Repositories
{
    internal sealed class OfferRepository : IOfferRepository
    {
        private readonly DbSet<Offer> _offers;

        public OfferRepository(IDbContext dbContext)
        {
            _offers = dbContext.Set<Offer>();
        }

        public async Task<Offer?> Get(int offerId)
         => await _offers.FirstOrDefaultAsync(x => x.Id == offerId);

        public void Add(Offer offer)
            => _offers.Add(offer);

        public void Update(Offer offer)
            => _offers.Update(offer);
    }
}

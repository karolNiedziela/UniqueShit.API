using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.FavouriteOffers;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Infrastructure.Persistence.Repositories
{
    internal sealed class FavouriteOfferRepository : IFavouriteOfferRepository
    {
        private readonly DbSet<FavouriteOffer> _favouriteOffers;

        public FavouriteOfferRepository(IDbContext dbContext)
        {
            _favouriteOffers = dbContext.Set<FavouriteOffer>();
        }

        public async Task<FavouriteOffer?> GetAsync(int id, Guid userId)
            => await _favouriteOffers.FirstOrDefaultAsync(x => x.Id == id && x.AppUserId == userId);

        public void Add(FavouriteOffer favouriteOffer)
            => _favouriteOffers.Add(favouriteOffer);

        public void Remove(FavouriteOffer favouriteOffer)
            => _favouriteOffers.Remove(favouriteOffer);
    }
}

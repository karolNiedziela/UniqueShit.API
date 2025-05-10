using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Offers;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Infrastructure.Persistence.Repositories
{
    internal sealed class PurchaseOfferRepository : IPurchaseOfferRepository
    {
        private readonly DbSet<PurchaseOffer> _purchaseOffers;

        public PurchaseOfferRepository(IDbContext dbContext)
        {
            _purchaseOffers = dbContext.Set<PurchaseOffer>();
        }

        public void Add(PurchaseOffer purchaseOffer)
            => _purchaseOffers.Add(purchaseOffer);
    }
}

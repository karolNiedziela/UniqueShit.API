using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Offers;

namespace UniqueShit.Domain.FavouriteOffers
{
    public sealed class FavouriteOffer : Entity
    {
        public int SaleOfferId { get; private set; }

        public SaleOffer SaleOffer { get; private set; } = default!;

        public Guid AppUserId { get; private set; }

        public AppUser AppUser { get; private set; } = default!;

        public DateTime CreatedOnUtc { get; } = DateTime.UtcNow;

        private FavouriteOffer() { }

        public FavouriteOffer(int saleOfferId, Guid appUserId)
        {
            SaleOfferId = saleOfferId;
            AppUserId = appUserId;
        }
    }
}

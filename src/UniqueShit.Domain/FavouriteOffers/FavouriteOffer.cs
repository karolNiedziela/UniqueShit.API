using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Offers;

namespace UniqueShit.Domain.FavouriteOffers
{
    public sealed class FavouriteOffer : Entity
    {
        public int OfferId { get; private set; }

        public Offer Offer { get; private set; } = default!;

        public Guid AppUserId { get; private set; }

        public AppUser AppUser { get; private set; } = default!;

        public DateTime CreatedOnUtc { get; } = DateTime.UtcNow;

        private FavouriteOffer() { }

        public FavouriteOffer(int offerId, Guid appUserId)
        {
            OfferId = offerId;
            AppUserId = appUserId;
        }
    }
}

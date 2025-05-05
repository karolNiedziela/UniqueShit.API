using UniqueShit.Domain.FavouriteOffers;

namespace UniqueShit.Domain.Repositories
{
    public interface IFavouriteOfferRepository
    {
        Task<FavouriteOffer?> GetAsync(int id, Guid userId);

        void Add(FavouriteOffer favouriteOffer);

        void Remove(FavouriteOffer favouriteOffer);
    }
}

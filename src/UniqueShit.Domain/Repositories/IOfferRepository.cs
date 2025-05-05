using UniqueShit.Domain.Offers;

namespace UniqueShit.Domain.Repositories
{
    public interface IOfferRepository
    {
        Task<Offer?> Get(int offerId);

        Task<bool> ActiveExistsAsync(int offerId);

        void Add(Offer offer);

        void Update(Offer offer);
    }
}

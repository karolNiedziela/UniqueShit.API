using UniqueShit.Domain.Offers;

namespace UniqueShit.Domain.Repositories
{
    public interface IOfferRepository
    {
        Task<Offer?> Get(int offerId);

        void Add(Offer offer);

        void Update(Offer offer);
    }
}

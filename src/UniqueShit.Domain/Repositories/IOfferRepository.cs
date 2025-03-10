using UniqueShit.Domain.Offers;

namespace UniqueShit.Domain.Repositories
{
    public interface IOfferRepository
    {
        void Add(Offer offer);

        void Update(Offer offer);
    }
}

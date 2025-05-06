using UniqueShit.Domain.Offers;

namespace UniqueShit.Domain.Repositories
{
    public interface ISaleOfferRepository
    {
        Task<SaleOffer?> Get(int saleOfferId);

        Task<bool> ActiveExistsAsync(int saleOfferId);

        void Add(SaleOffer saleOffer);

        void Update(SaleOffer saleOffer);
    }
}

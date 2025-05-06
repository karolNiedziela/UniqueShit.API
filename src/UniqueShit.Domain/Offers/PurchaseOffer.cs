using UniqueShit.Domain.Offers.ValueObjects;

namespace UniqueShit.Domain.Offers
{
    public sealed class PurchaseOffer : Offer
    {
        private PurchaseOffer() : base() { }

        public PurchaseOffer(
            Topic topic,
            Description description,
            Guid appUserId,
            int modelId) : base(topic, description, modelId, appUserId)
        {
        }
    }
}

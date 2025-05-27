using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Enumerations;
using UniqueShit.Domain.Offers.ValueObjects;

namespace UniqueShit.Domain.Offers
{
    public sealed class SaleOffer : Offer
    {
        public Money Price { get; private set; } = default!;

        public int ItemConditionId { get; private set; }

        public int SizeId { get; private set; }

        public Size Size { get; private set; } = default!;

        public int DeliveryTypeId { get; private set; } = default!;

        public int PaymentTypeId { get; private set; } = default!;

        public List<Colour> Colours { get; private set; } = [];

        public int Quantity { get; private set; }

        private SaleOffer() : base() { }

        public SaleOffer(
            Topic topic,
            Description description,
            Money price,
            Guid appUserId,
            int itemConditionId,
            int sizeId,
            int modelId,
            int deliveryTypeId,
            int paymentTypeId,
            int quantity = 1) : base(topic, description, modelId, appUserId)
        {
            Price = price;
            ItemConditionId = itemConditionId;
            SizeId = sizeId;
            DeliveryTypeId = deliveryTypeId;
            PaymentTypeId = paymentTypeId;
            Quantity = quantity;
        }
        public void AddColours(IEnumerable<Colour> colours)
               => Colours.AddRange(colours);
    }
}

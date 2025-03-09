using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.Offers.ValueObjects;

namespace UniqueShit.Domain.Offers
{
    public sealed class Offer : Entity
    {
        private const int DefaultPeriodOfOfferValidityInDays = 30;

        public Topic Topic { get; private set; } = default!;

        public Description Description { get; private set; } = default!;

        public int Quantity { get; private set; }

        public Money Price { get; private set; } = default!;

        public DateTime CreatedOnUtc { get; } = DateTime.UtcNow;

        public DateTime? ExpiredAtUtc { get; private set; } = DateTime.Now.AddDays(DefaultPeriodOfOfferValidityInDays);

        public int OfferTypeId { get; private set; }

        public int ItemConditionId { get; private set; }

        public int SizeId { get; private set; }

        public int ModelId { get; private set; }


        private Offer() { }

        private Offer(
            Topic topic,
            Description description,
            Money price,
            int itemConditionId,
            int sizeId,
            int modelId,
            int offerTypeId,
            int quantity = 1)
        {
            Topic = topic;
            Description = description;
            Price = price;
            ItemConditionId = itemConditionId;
            Quantity = quantity;
            OfferTypeId = offerTypeId;
            SizeId = sizeId;
            ModelId = modelId;
        }

        public static Offer CreatePurchaseOffer(
            Topic topic,
            Description description,
            Money price,
            int itemConditionId,
            int sizeId,
            int modelId,
            int quantity = 1)
            => new(
                topic: topic,
                description: description,
                price: price,
                itemConditionId: itemConditionId,
                sizeId: sizeId,
                modelId: modelId,
                offerTypeId: Enumerations.OfferType.Purchase.Id,
                quantity: quantity);

        public static Offer CreateSaleOffer(
            Topic topic,
            Description description,
            Money price,
            int itemConditionId,
            int sizeId,
            int modelId,
            int quantity = 1)
            => new(
                topic: topic,
                description: description,
                price: price,
                itemConditionId: itemConditionId,
                sizeId: sizeId,
                modelId: modelId,
                offerTypeId: Enumerations.OfferType.Sale.Id,
                quantity: quantity);
    }
}

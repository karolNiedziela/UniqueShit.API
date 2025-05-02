using System.Drawing;
using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Enumerations;
using UniqueShit.Domain.Offers.Enumerations;
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

        public DateTime? ExpiredAtUtc { get; private set; } = DateTime.UtcNow.AddDays(DefaultPeriodOfOfferValidityInDays);

        public int OfferTypeId { get; private set; }

        public int ItemConditionId { get; private set; }

        public int SizeId { get; private set; }

        public int ModelId { get; private set; }

        public Model Model { get; private set; } = default!;

        public int OfferStateId { get; private set; }

        public Guid AppUserId { get; private set; }

        public AppUser AppUser { get; private set; } = default!;

        public int DeliveryTypeId { get; private set; } = default!;

        public int PaymentTypeId { get; private set; } = default!;

        public List<Colour> Colours { get; private set; } = [];

        private Offer() { }

        private Offer(
            Topic topic,
            Description description,
            Money price,
            Guid appUserId,
            int offerTypeId,
            int itemConditionId,
            int sizeId,
            int modelId,
            int deliveryTypeId,
            int paymentTypeId,
            int quantity = 1)
        {
            Topic = topic;
            Description = description;
            Price = price;
            AppUserId = appUserId;
            ItemConditionId = itemConditionId;
            Quantity = quantity;
            OfferTypeId = offerTypeId;
            SizeId = sizeId;
            ModelId = modelId;
            DeliveryTypeId = deliveryTypeId;
            PaymentTypeId = paymentTypeId;
            OfferStateId = OfferState.Active.Id;
        }

        public static Offer CreatePurchaseOffer(
            Topic topic,
            Description description,
            Money price,
            Guid appUserId,
            int itemConditionId,
            int sizeId,
            int modelId,
            int deliveryTypeId,
            int paymentTypeId,
            int quantity = 1)
            => new(
                topic: topic,
                description: description,
                price: price,
                appUserId: appUserId,
                offerTypeId: OfferType.Purchase.Id,
                itemConditionId: itemConditionId,
                sizeId: sizeId,
                modelId: modelId,
                deliveryTypeId: deliveryTypeId,
                paymentTypeId: paymentTypeId,
                quantity: quantity);

        public static Offer CreateSaleOffer(
            Topic topic,
            Description description,
            Money price,
            Guid appUserId,
            int itemConditionId,
            int sizeId,
            int modelId,
            int deliveryTypeId,
            int paymentTypeId,
            int quantity = 1)
            => new(
                topic: topic,
                description: description,
                price: price,
                appUserId: appUserId,
                offerTypeId: OfferType.Sale.Id,
                itemConditionId: itemConditionId,
                sizeId: sizeId,
                modelId: modelId,
                deliveryTypeId: deliveryTypeId,
                paymentTypeId: paymentTypeId,
                quantity: quantity);

        public void AddColours(IEnumerable<Colour> colours)
                => Colours.AddRange(colours);
    }
}

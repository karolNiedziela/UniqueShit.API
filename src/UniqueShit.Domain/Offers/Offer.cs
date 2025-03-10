using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Enumerations;
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

        public int ManufacturerId { get; private set; }

        public int ProductCategoryId { get; private set; }

        public int OfferStateId { get; private set; }

        public Manufacturer Manufacturer { get; private set; } = default!;

        public List<Colour> Colours { get; private set; } = [];

        private Offer() { }

        private Offer(
            Topic topic,
            Description description,
            Money price,
            int offerTypeId,
            int itemConditionId,
            int sizeId,
            int productCategoryId,
            int manufacturerId,
            int quantity = 1)
        {
            Topic = topic;
            Description = description;
            Price = price;
            ItemConditionId = itemConditionId;
            Quantity = quantity;
            OfferTypeId = offerTypeId;
            SizeId = sizeId;
            ProductCategoryId = productCategoryId;
            ManufacturerId = manufacturerId;
            OfferStateId = OfferState.Active.Id;
        }

        public static Offer CreatePurchaseOffer(
            Topic topic,
            Description description,
            Money price,
            int itemConditionId,
            int sizeId,
            int productCategoryId,
            int manufacturerId,
            int quantity = 1)
            => new(
                topic: topic,
                description: description,
                price: price,
                offerTypeId: OfferType.Purchase.Id,
                itemConditionId: itemConditionId,
                sizeId: sizeId,
                productCategoryId: productCategoryId,
                manufacturerId: manufacturerId,
                quantity: quantity);

        public static Offer CreateSaleOffer(
            Topic topic,
            Description description,
            Money price,
            int itemConditionId,
            int sizeId,
            int productCategoryId,
            int manufacturerId,
            int quantity = 1)
            => new(
                topic: topic,
                description: description,
                price: price,
                offerTypeId: OfferType.Sale.Id,
                itemConditionId: itemConditionId,
                sizeId: sizeId,
                productCategoryId: productCategoryId,
                manufacturerId: manufacturerId,
                quantity: quantity);

        public void AddColours(List<Colour> colours)
            => Colours.AddRange(colours);
    }
}

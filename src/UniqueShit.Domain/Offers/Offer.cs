using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Offers.Enumerations;
using UniqueShit.Domain.Offers.ValueObjects;

namespace UniqueShit.Domain.Offers
{
    public abstract class Offer : Entity
    {
        protected const int DefaultPeriodOfOfferValidityInDays = 30;

        public Topic Topic { get; protected set; } = default!;

        public Description Description { get; protected set; } = default!;

        public int ModelId { get; protected set; }

        public Model Model { get; protected set; } = default!;

        public Guid AppUserId { get; protected set; }

        public AppUser AppUser { get; protected set; } = default!;

        public DateTime CreatedOnUtc { get; } = DateTime.UtcNow;

        public DateTime? ExpiredAtUtc { get; private set; } = DateTime.UtcNow.AddDays(DefaultPeriodOfOfferValidityInDays);

        public int OfferStateId { get; protected set; }

        protected Offer() { }

        protected Offer(
            Topic topic,
            Description description,
            int modelId, 
            Guid appUserId)
        {
            Topic = topic;
            Description = description;
            ModelId = modelId;
            AppUserId = appUserId;
            OfferStateId = OfferState.Active.Id;
        }
    }
}

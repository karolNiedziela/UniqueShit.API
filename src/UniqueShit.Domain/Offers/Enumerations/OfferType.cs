using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Domain.Offers.Enumerations
{
    public sealed class OfferType : Enumeration<OfferType>
    {
        public static readonly OfferType Purchase = new(1, nameof(Purchase));
        public static readonly OfferType Sale = new(2, nameof(Sale));

        public OfferType(int id, string name) : base(id, name)
        {
        }
    }
}

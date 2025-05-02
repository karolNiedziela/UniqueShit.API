using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Domain.Offers.Enumerations
{
    public sealed class OfferState : Enumeration<OfferState>
    {
        public static readonly OfferState Active = new(1, nameof(Active));
        public static readonly OfferState Expired = new(2, nameof(Expired));
        public static readonly OfferState Completed = new(3, nameof(Completed));

        public OfferState(int id, string name) : base(id, name)
        {
        }
    }
}

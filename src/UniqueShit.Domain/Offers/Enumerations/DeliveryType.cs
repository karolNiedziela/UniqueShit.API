using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Domain.Offers.Enumerations
{
    public sealed class DeliveryType : Enumeration<DeliveryType>
    {
        public static readonly DeliveryType Meeting = new(1, nameof(Meeting));
        public static readonly DeliveryType Shipping = new(2, nameof(Shipping));
        public static readonly DeliveryType Any = new(99, nameof(Any));

        public DeliveryType(int id, string name) : base(id, name)
        {            
        }
    }
}

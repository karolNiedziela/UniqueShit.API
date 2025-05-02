using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Domain.Offers.Enumerations
{
    public sealed class PaymentType : Enumeration<PaymentType>
    {
        public static readonly PaymentType BankTransfer = new(1, "Bank transfer");
        public static readonly PaymentType Cash = new(2, "Cash");
        public static readonly PaymentType Blik = new(3, "Blik");
        public static readonly PaymentType Any = new(4, "Any");

        public PaymentType(int id, string name) : base(id, name)
        {
        }
    }
}

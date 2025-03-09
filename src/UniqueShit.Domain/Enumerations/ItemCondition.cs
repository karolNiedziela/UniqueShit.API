using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Domain.Enumerations
{
    public sealed class ItemCondition : Enumeration<ItemCondition>
    {
        public static readonly ItemCondition New = new(1, nameof(New));
        public static readonly ItemCondition Used = new(2, nameof(Used));
        public static readonly ItemCondition Damaged = new(3, nameof(Damaged));

        public ItemCondition(int id, string name) : base(id, name)
        {
        }
    }
}

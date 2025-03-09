using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Domain.Enumerations
{
    public sealed class Colour : Enumeration<Colour>
    {
        public static readonly Colour Black = new(1, nameof(Black));
        public static readonly Colour White = new(2, nameof(White));
        public static readonly Colour Red = new(3, nameof(Red));
        public static readonly Colour Blue = new(4, nameof(Blue));
        public static readonly Colour Green = new(5, nameof(Green));

        public Colour(int id, string name) : base(id, name)
        {
        }
    }
}

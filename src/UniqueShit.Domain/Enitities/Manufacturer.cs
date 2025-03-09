using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Domain.Enitities
{
    public sealed class Manufacturer : Entity
    {
        public string Name { get; private set; } = default!;

        public Manufacturer()
        {
        }

        public Manufacturer(string name)
        {
            Name = name;
        }
    }
}

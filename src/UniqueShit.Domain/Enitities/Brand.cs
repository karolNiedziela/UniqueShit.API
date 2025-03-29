using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Domain.Enitities
{
    public sealed class Brand : Entity
    {
        public string Name { get; private set; } = default!;

        public Brand()
        {
        }

        public Brand(string name)
        {
            Name = name;
        }
    }
}

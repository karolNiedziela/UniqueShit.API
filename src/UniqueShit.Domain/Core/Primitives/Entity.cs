namespace UniqueShit.Domain.Core.Primitives
{
    public abstract class Entity
    {
        public int Id { get; protected set; }

        protected Entity() { }
    }
}

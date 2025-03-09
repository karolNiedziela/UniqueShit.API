using UniqueShit.Domain.Core.Errors;
using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Domain.Offers.ValueObjects
{
    public sealed class Topic : ValueObject
    {
        public const int MaxLength = 30;

        public string Value { get; } = default!;

        private Topic() { }

        private Topic(string value) => Value = value;

        public static Result<Topic> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return DomainErrors.Topic.NullOrEmpty;
            }

            if (value.Length > MaxLength)
            {
                return DomainErrors.Topic.LongerThanAllowed;
            }

            return new Topic(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}

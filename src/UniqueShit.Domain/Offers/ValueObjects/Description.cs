using UniqueShit.Domain.Core.Errors;
using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Domain.Offers.ValueObjects
{
    public sealed class Description : ValueObject
    {
        public const int MaxLength = 200;

        public string Value { get; } = default!;

        private Description() { }

        private Description(string value) => Value = value;

        public static Result<Description> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return DomainErrors.Description.NullOrEmpty;
            }

            if (value.Length > MaxLength)
            {
                return DomainErrors.Description.LongerThanAllowed;
            }

            return new Description(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}

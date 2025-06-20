using System.Text.RegularExpressions;
using UniqueShit.Domain.Core.Errors;
using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Domain.ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        public const int MaxLength = 9;

        public const string Pattern = @"^\d{9}$";

        public string? Value { get; }

        private PhoneNumber() { }

        private PhoneNumber(string? value) => Value = value;

        public static Result<PhoneNumber> Create(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new PhoneNumber(null);
            }

            if (value.Length > MaxLength)
            {
                return DomainErrors.PhoneNumber.LongerThanAllowed;
            }

            bool isValid = Regex.IsMatch(value, Pattern);
            if (!isValid)
            {
                return DomainErrors.PhoneNumber.InvalidFormat;
            }

            return new PhoneNumber("+48" + value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value is null ? null! : Value!;
        }

        public static implicit operator string?(PhoneNumber phoneNumber)
        {
            return phoneNumber?.Value;
        }
    }
}

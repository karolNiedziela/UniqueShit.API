using UniqueShit.Domain.Core.Errors;
using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Domain.Offers.ValueObjects
{
    public sealed class Money : ValueObject
    {
        private static readonly List<string> ValidCurrencies = ["PLN"];

        public const int CurrencyMaxLength = 3;

        public decimal Amount { get; }

        public string Currency { get; } = default!;

        private Money() { }

        private Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Result<Money> Create(decimal amount, string currency)
        {
            if (amount < 0)
            {
                return DomainErrors.Money.NegativeAmount;
            }

            if (string.IsNullOrWhiteSpace(currency))
            {
                return DomainErrors.Money.EmptyCurrency;
            }

            if (!ValidCurrencies.Contains(currency))
            {
                return DomainErrors.Money.InvalidCurrency;
            }

            return new Money(amount, currency);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}

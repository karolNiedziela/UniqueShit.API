using UniqueShit.Domain.Core.Primitives;

namespace UniqueShit.Domain.Core.Errors
{
    public static class DomainErrors
    {
        public static class Topic
        {
            public static Error NullOrEmpty => new("Topic.NullOrEmpty", "The topic is required.");

            public static Error LongerThanAllowed => new("Topic.LongerThanAllowed", "The topic is longer than allowed.");
        }

        public static class Description
        {
            public static Error NullOrEmpty => new("Description.NullOrEmpty", "The description is required.");

            public static Error LongerThanAllowed => new("Description.LongerThanAllowed", "The description is longer than allowed.");
        }

        public static class Money
        {
            public static Error NegativeAmount => new("Money.NegativeAmount", "Amount cannot be negative.");

            public static Error EmptyCurrency => new("Money.NullOrEmptyCurrency", "Currency is required.");

            public static Error InvalidCurrency => new("Money.InvalidCurrency", "This currency is not supported.");
        }

        public static class Offer
        {
            public static Error SizeNotFound => new("Offer.SizeNotFound", "Size with the specified identifier was not found.");

            public static Error ModelNotFound => new("Offer.ModelNotFound", "Model with the specified identifier was not found.");
        }
    }
}

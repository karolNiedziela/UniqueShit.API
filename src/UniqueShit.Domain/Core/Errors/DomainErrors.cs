using UniqueShit.Domain.Core.Primitives;
using UniqueShit.Domain.Offers;

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

        public static class SaleOffer
        {
            public static Error SizeNotFound => new("SaleOffer.SizeNotFound", "Size with the specified identifier was not found.");

            public static Error ModelNotFound => new("SaleOffer.ModelNotFound", "Model with the specified identifier was not found.");

            public static Error SaleOfferNotFound => new("OfSaleOfferfer.OfferNotFound", "Sale offer with the specified identifier was not found.");

            public static Error NoPrivilegesToRemove => new("SaleOffer.NoPrivilegesToRemove", "You have no privileges to remove this offer.", ErrorType.Forbidden);
        }

        public static class PurchaseOffer
        {
            public static Error ModelNotFound => new("PurchaseOffer.ModelNotFound", "Model with the specified identifier was not found.");
        }

        public static class Model
        {
            public static Error BrandNotFound => new("Model.BrandNotFound", "Brand with the specified identifier was not found.");
        }

        public static class FavouriteOffer
        {
            public static Error FavouriteOfferNotFound => new("FavouriteOffer.NotFound", "Favourite offer with the specified identifier was not found.");

            public static Error OfferNotFound => new("FavouriteOffer.OfferNotFound", "Offer with the specified identifier was not found.");
        }
    }
}

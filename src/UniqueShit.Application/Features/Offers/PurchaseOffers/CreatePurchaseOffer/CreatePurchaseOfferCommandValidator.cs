using FluentValidation;

namespace UniqueShit.Application.Features.Offers.PurchaseOffers.CreatePurchaseOffer
{
    internal sealed class CreatePurchaseOfferCommandValidator : AbstractValidator<CreatePurchaseOfferCommand>
    {
        public CreatePurchaseOfferCommandValidator()
        {
            RuleFor(x => x.Topic)
              .NotEmpty()
              .WithMessage("Topic is required.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.");
            RuleFor(x => x.ModelId)
              .GreaterThan(0)
              .WithMessage("Model is required.");
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueShit.Application.Features.Offers.SaleOffers.Commands.CreateSaleOffer
{
    internal sealed class CreateSaleOfferCommandValidator : AbstractValidator<CreateSaleOfferCommand>
    {
        public CreateSaleOfferCommandValidator()
        {
            RuleFor(x => x.Topic)
                .NotEmpty()
                .WithMessage("Topic is required.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.");
            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("Price is required.");

            RuleFor(x => x.ItemConditionId)
                .GreaterThan(0)
                .WithMessage("Item condition is required.");
            RuleFor(x => x.ModelId)
                .GreaterThan(0)
                .WithMessage("Model is required.");
            RuleFor(x => x.SizeId)
                .GreaterThan(0)
                .WithMessage("Size is required.");
            RuleFor(x => x.DeliveryTypeId)
                .GreaterThan(0)
                .WithMessage("Delivery type is required.");
            RuleFor(x => x.PaymentTypeId)
                .GreaterThan(0)
                .WithMessage("Payment type is required.");
            RuleFor(x => x.ColourIds)
                .NotNull()
                .WithMessage("At least one colour is required.");

            RuleFor(x => x.ColourIds)
                .Must(colourIds => colourIds.All(id => id > 0))
                .When(x => x.ColourIds != null)
                .WithMessage("Invalid colour.");
        }
    }
}

using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Core.Errors;
using UniqueShit.Domain.Core.Primitives.Results;
using UniqueShit.Domain.Enumerations;
using UniqueShit.Domain.Offers;
using UniqueShit.Domain.Offers.ValueObjects;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Application.Features.Offers.Commands.CreateOffer
{
    public sealed class CreateOfferCommandHandler : ICommandHandler<CreateOfferCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly ISizeRepository _sizeRepository;

        public CreateOfferCommandHandler(
            IUnitOfWork unitOfWork,
            IManufacturerRepository manufacturerRepository,
            IOfferRepository offerRepository,
            ISizeRepository sizeRepository)
        {
            _unitOfWork = unitOfWork;
            _manufacturerRepository = manufacturerRepository;
            _offerRepository = offerRepository;
            _sizeRepository = sizeRepository;
        }

        public async Task<Result<int>> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            var colourResults = request.ColourIds.Select(Colour.FromValue).ToList();
            var itemCondition = ItemCondition.FromValue(request.ItemConditionId);
            var offerType = OfferType.FromValue(request.OfferTypeId);
            var productCategory = ProductCategory.FromValue(request.ProductCategoryId);

            var enumerationsValidationResult = Result.Success()
                .AggregateErrors(colourResults[0], itemCondition, offerType, productCategory);
            if (enumerationsValidationResult.IsFailure)
            {
                return enumerationsValidationResult.Errors;
            }

            var topic = Topic.Create(request.Topic);
            var description = Description.Create(request.Description);
            var price = Money.Create(request.Price.Amount, request.Price.Currency);

            var valueObjectsValidationResult = Result.Success().AggregateErrors(topic, description, price);
            if (valueObjectsValidationResult.IsFailure)
            {
                return valueObjectsValidationResult.Errors;
            }

            var manufacturer = await _manufacturerRepository.GetAsync(request.ManufacturerId);
            if (manufacturer is null)
            {
                return DomainErrors.Offer.ManufacturerNotFound;
            }

            var size = await _sizeRepository.GetAsync(request.SizeId, request.ProductCategoryId);
            if (size is null)
            {
                return DomainErrors.Offer.SizeNotFound;
            }

            var colours = colourResults.Select(x => x.Value).ToList();

            var offer = offerType.Value.Id == OfferType.Purchase.Id ?
                Offer.CreatePurchaseOffer(topic.Value, description.Value, price.Value, itemCondition.Value.Id, size.Id, productCategory.Value.Id, manufacturer.Id, request.Quantity) :
                Offer.CreateSaleOffer(topic.Value, description.Value, price.Value, itemCondition.Value.Id, size.Id, productCategory.Value.Id, manufacturer.Id, request.Quantity);

            _offerRepository.Add(offer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            offer.AddColours(colours);

            _offerRepository.Update(offer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return offer.Id;
        }
    }
}

using UniqueShit.Application.Core.Authentication;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Core.Errors;
using UniqueShit.Domain.Core.Primitives.Results;
using UniqueShit.Domain.Enumerations;
using UniqueShit.Domain.Offers;
using UniqueShit.Domain.Offers.Enumerations;
using UniqueShit.Domain.Offers.ValueObjects;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Application.Features.Offers.Commands.CreateOffer
{
    public sealed class CreateOfferCommandHandler : ICommandHandler<CreateOfferCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOfferRepository _offerRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        public CreateOfferCommandHandler(
            IUnitOfWork unitOfWork,
            IOfferRepository offerRepository,
            ISizeRepository sizeRepository,
            IModelRepository modelRepository,
            IUserIdentifierProvider userIdentifierProvider)
        {
            _unitOfWork = unitOfWork;
            _offerRepository = offerRepository;
            _sizeRepository = sizeRepository;
            _modelRepository = modelRepository;
            _userIdentifierProvider = userIdentifierProvider;
        }

        public async Task<Result<int>> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            var validationResult = ValidateRequest(request);
            if (validationResult.IsFailure)
            {
                return validationResult.Errors;
            }

            var model = await _modelRepository.GetAsync(request.ModelId);
            if (model is null)
            {
                return DomainErrors.Offer.ModelNotFound;
            }

            var size = await _sizeRepository.GetAsync(request.SizeId, model.ProductCategoryId);
            if (size is null)
            {
                return DomainErrors.Offer.SizeNotFound;
            }

            var offer = CreateOffer(request, model.Id, size.Id);
            _offerRepository.Add(offer);

            var colours = request.ColourIds
               .Select(Colour.FromValue)
               .Select(x => x.Value)
               .ToArray();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            offer.AddColours(colours);

            _offerRepository.Update(offer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return offer.Id;
        }

        private Result ValidateRequest(CreateOfferCommand request)
        {
            var itemCondition = ItemCondition.FromValue(request.ItemConditionId);
            var offerType = OfferType.FromValue(request.OfferTypeId);
            var deliveryType = DeliveryType.FromValue(request.DeliveryTypeId);
            var paymentType = PaymentType.FromValue(request.PaymentTypeId);
            var colours = request.ColourIds
                .Select(Colour.FromValue)
                .ToArray();

            var enumerationsValidationResult = Result.Success()
                .AggregateErrors([itemCondition, offerType, deliveryType, paymentType, ..colours]); 
            if (enumerationsValidationResult.IsFailure)
            {
                return enumerationsValidationResult;
            }

            var topic = Topic.Create(request.Topic);
            var description = Description.Create(request.Description);
            var price = Money.Create(request.Price.Amount, request.Price.Currency);

            var valueObjectsValidationResult = Result.Success().AggregateErrors([topic, description, price]);
            if (valueObjectsValidationResult.IsFailure)
            {
                return valueObjectsValidationResult;
            }

            return Result.Success();
        }

        private Offer CreateOffer(CreateOfferCommand request, int modelId, int sizeId)
        {
            var topic = Topic.Create(request.Topic).Value;
            var description = Description.Create(request.Description).Value;
            var price = Money.Create(request.Price.Amount, request.Price.Currency).Value;
            var itemConditionId = ItemCondition.FromValue(request.ItemConditionId).Value.Id;
            var offerTypeId = OfferType.FromValue(request.OfferTypeId).Value.Id;
            var deliveryTypeId = DeliveryType.FromValue(request.DeliveryTypeId).Value.Id;
            var paymentTypeId = PaymentType.FromValue(request.PaymentTypeId).Value.Id;

            return offerTypeId == OfferType.Purchase.Id
                ? Offer.CreatePurchaseOffer(topic, description, price, _userIdentifierProvider.UserId, itemConditionId, sizeId,
                modelId, deliveryTypeId, paymentTypeId, request.Quantity)
                : Offer.CreateSaleOffer(topic, description, price, _userIdentifierProvider.UserId, itemConditionId, sizeId, modelId, deliveryTypeId, paymentTypeId, request.Quantity);
        }
    }
}

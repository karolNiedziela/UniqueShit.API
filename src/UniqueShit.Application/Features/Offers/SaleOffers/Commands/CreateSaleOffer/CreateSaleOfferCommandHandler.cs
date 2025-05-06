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

namespace UniqueShit.Application.Features.Offers.SaleOffers.Commands.CreateSaleOffer
{
    public sealed class CreateSaleOfferCommandHandler : ICommandHandler<CreateSaleOfferCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleOfferRepository _saleOfferRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        public CreateSaleOfferCommandHandler(
            IUnitOfWork unitOfWork,
            ISaleOfferRepository saleOfferRepository,
            ISizeRepository sizeRepository,
            IModelRepository modelRepository,
            IUserIdentifierProvider userIdentifierProvider)
        {
            _unitOfWork = unitOfWork;
            _saleOfferRepository = saleOfferRepository;
            _sizeRepository = sizeRepository;
            _modelRepository = modelRepository;
            _userIdentifierProvider = userIdentifierProvider;
        }

        public async Task<Result<int>> Handle(CreateSaleOfferCommand request, CancellationToken cancellationToken)
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

            var saleOffer = CreateSaleOffer(request, model.Id, size.Id);
            _saleOfferRepository.Add(saleOffer);

            var colours = request.ColourIds
               .Select(Colour.FromValue)
               .Select(x => x.Value)
               .ToArray();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            saleOffer.AddColours(colours);

            _saleOfferRepository.Update(saleOffer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return saleOffer.Id;
        }

        private Result ValidateRequest(CreateSaleOfferCommand request)
        {
            var itemCondition = ItemCondition.FromValue(request.ItemConditionId);
            var deliveryType = DeliveryType.FromValue(request.DeliveryTypeId);
            var paymentType = PaymentType.FromValue(request.PaymentTypeId);
            var colours = request.ColourIds
                .Select(Colour.FromValue)
                .ToArray();

            var enumerationsValidationResult = Result.Success()
                .AggregateErrors([itemCondition, deliveryType, paymentType, .. colours]);
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

        private SaleOffer CreateSaleOffer(CreateSaleOfferCommand request, int modelId, int sizeId)
        {
            var topic = Topic.Create(request.Topic).Value;
            var description = Description.Create(request.Description).Value;
            var price = Money.Create(request.Price.Amount, request.Price.Currency).Value;
            var itemConditionId = ItemCondition.FromValue(request.ItemConditionId).Value.Id;
            var deliveryTypeId = DeliveryType.FromValue(request.DeliveryTypeId).Value.Id;
            var paymentTypeId = PaymentType.FromValue(request.PaymentTypeId).Value.Id;

            return new SaleOffer(topic, description, price, _userIdentifierProvider.UserId, itemConditionId, sizeId, modelId, deliveryTypeId, paymentTypeId, request.Quantity);
        }
    }
}

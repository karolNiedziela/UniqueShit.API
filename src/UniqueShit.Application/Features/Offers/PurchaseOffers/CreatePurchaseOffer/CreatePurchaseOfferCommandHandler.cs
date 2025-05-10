using UniqueShit.Application.Core.Authentication;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Core.Errors;
using UniqueShit.Domain.Core.Primitives.Results;
using UniqueShit.Domain.Offers;
using UniqueShit.Domain.Offers.ValueObjects;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Application.Features.Offers.PurchaseOffers.CreatePurchaseOffer
{
    public sealed class CreatePurchaseOfferCommandHandler : ICommandHandler<CreatePurchaseOfferCommand, Result<int>>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IModelRepository _modelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPurchaseOfferRepository _purchaseOfferRepository;

        public CreatePurchaseOfferCommandHandler(
            IUserIdentifierProvider userIdentifierProvider,
            IModelRepository modelRepository,
            IUnitOfWork unitOfWork,
            IPurchaseOfferRepository purchaseOfferRepository)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _modelRepository = modelRepository;
            _unitOfWork = unitOfWork;
            _purchaseOfferRepository = purchaseOfferRepository;
        }

        public async Task<Result<int>> Handle(CreatePurchaseOfferCommand request, CancellationToken cancellationToken)
        {
            var validationResult = ValidateRequest(request);
            if (validationResult.IsFailure)
            {
                return validationResult.Errors;
            }

            var topic = Topic.Create(request.Topic).Value;
            var description = Description.Create(request.Description).Value;

            var model = await _modelRepository.GetAsync(request.ModelId);
            if (model is null)
            {
                return DomainErrors.PurchaseOffer.ModelNotFound;
            }

            var purchaseOffer = new PurchaseOffer(
                topic,
                description,
                _userIdentifierProvider.UserId,
                request.ModelId);
            _purchaseOfferRepository.Add(purchaseOffer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return purchaseOffer.Id;
        }

        private Result ValidateRequest(CreatePurchaseOfferCommand request)
        {
            var topic = Topic.Create(request.Topic);
            var description = Description.Create(request.Description);

            var valueObjectsValidationResult = Result.Success().AggregateErrors([topic, description]);
            if (valueObjectsValidationResult.IsFailure)
            {
                return valueObjectsValidationResult;
            }


            return Result.Success();
        }
    }
}

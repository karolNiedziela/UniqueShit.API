using MediatR;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Enumerations;
using UniqueShit.Domain.Offers;
using UniqueShit.Domain.Offers.ValueObjects;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Application.Features.Offers.CreateOffer
{
    public sealed class CreateOfferCommandHandler : ICommandHandler<CreateOfferCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModelRepository _modelRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly ISizeRepository _sizeRepository;

        public CreateOfferCommandHandler(
            IUnitOfWork unitOfWork,
            IModelRepository modelRepository,
            IOfferRepository offerRepository,
            ISizeRepository sizeRepository)
        {
            _unitOfWork = unitOfWork;
            _modelRepository = modelRepository;
            _offerRepository = offerRepository;
            _sizeRepository = sizeRepository;
        }

        public async Task<int> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            var colour = Colour.FromValue(request.ColourId);
            var itemCondition = ItemCondition.FromValue(request.ItemConditionId);
            var offerType = OfferType.FromValue(request.OfferTypeId);
            var productCategory = ProductCategory.FromValue(request.ProductCategoryId);

            var topic = Topic.Create(request.Topic);
            var description = Description.Create(request.Description);
            var price = Money.Create(request.Price.Amount, request.Price.Currency);

            var size = await _sizeRepository.GetAsync(request.SizeId, request.ProductCategoryId);

            var model = await _modelRepository.GetAsync(request.ModelId);

            var offer = offerType.Id == OfferType.Purchase.Id ?
                Offer.CreatePurchaseOffer(topic.Value, description.Value, price.Value, itemCondition.Id, colour.Id, model.Id, request.Quantity) :
                Offer.CreateSaleOffer(topic.Value, description.Value,price.Value, itemCondition.Id, colour.Id, model.Id, request.Quantity);

            _offerRepository.Add(offer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return offer.Id;
        }
    }
}

using UniqueShit.Application.Core.Authentication;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Core.Errors;
using UniqueShit.Domain.Core.Primitives.Results;
using UniqueShit.Domain.FavouriteOffers;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Application.Features.FavouriteOffers.Commands.AddFavouriteOffer
{
    public sealed class AddFavouriteOfferCommandHandler : ICommandHandler<AddFavouriteOfferCommand, Result>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly ISaleOfferRepository _saleOfferRepository;
        private readonly IFavouriteOfferRepository _favouriteOfferRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddFavouriteOfferCommandHandler(IUserIdentifierProvider userIdentifierProvider, ISaleOfferRepository saleOfferRepository, IFavouriteOfferRepository favouriteOfferRepository, IUnitOfWork unitOfWork)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _saleOfferRepository = saleOfferRepository;
            _favouriteOfferRepository = favouriteOfferRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddFavouriteOfferCommand request, CancellationToken cancellationToken)
        {
            if (!await _saleOfferRepository.ActiveExistsAsync(request.OfferId))
            {
                return DomainErrors.FavouriteOffer.OfferNotFound;
            }

            var favouriteOffer = new FavouriteOffer(request.OfferId, _userIdentifierProvider.UserId);

            _favouriteOfferRepository.Add(favouriteOffer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

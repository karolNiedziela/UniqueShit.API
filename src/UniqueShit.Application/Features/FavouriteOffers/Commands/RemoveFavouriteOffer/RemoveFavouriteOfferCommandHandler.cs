using UniqueShit.Application.Core.Authentication;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Core.Errors;
using UniqueShit.Domain.Core.Primitives.Results;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Application.Features.FavouriteOffers.Commands.RemoveFavouriteOffer
{
    public sealed class RemoveFavouriteOfferCommandHandler : ICommandHandler<RemoveFavouriteOfferCommand, Result>
    {
        private readonly IFavouriteOfferRepository _favouriteOfferRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        public RemoveFavouriteOfferCommandHandler(IFavouriteOfferRepository favouriteOfferRepository, IUnitOfWork unitOfWork, IUserIdentifierProvider userIdentifierProvider)
        {
            _favouriteOfferRepository = favouriteOfferRepository;
            _unitOfWork = unitOfWork;
            _userIdentifierProvider = userIdentifierProvider;
        }

        public async Task<Result> Handle(RemoveFavouriteOfferCommand request, CancellationToken cancellationToken)
        {
            var favouriteOffer = await _favouriteOfferRepository.GetAsync(request.Id, _userIdentifierProvider.UserId);
            if (favouriteOffer is null)
            {
                return DomainErrors.FavouriteOffer.FavouriteOfferNotFound;
            }

            _favouriteOfferRepository.Remove(favouriteOffer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

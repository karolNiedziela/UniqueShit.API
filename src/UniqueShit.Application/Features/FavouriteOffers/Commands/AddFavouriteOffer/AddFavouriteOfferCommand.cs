using UniqueShit.Application.Core.Messaging;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Application.Features.FavouriteOffers.Commands.AddFavouriteOffer
{
    public sealed record AddFavouriteOfferCommand(int OfferId) : ICommand<Result>;    
}

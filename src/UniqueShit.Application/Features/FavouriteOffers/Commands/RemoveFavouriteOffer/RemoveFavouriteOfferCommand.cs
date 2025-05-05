using UniqueShit.Application.Core.Messaging;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Application.Features.FavouriteOffers.Commands.RemoveFavouriteOffer
{
    public sealed record RemoveFavouriteOfferCommand(int Id) : ICommand<Result>;
}

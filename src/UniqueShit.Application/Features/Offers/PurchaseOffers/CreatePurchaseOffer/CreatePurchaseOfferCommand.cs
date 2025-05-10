using UniqueShit.Application.Core.Messaging;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Application.Features.Offers.PurchaseOffers.CreatePurchaseOffer
{
    public sealed record CreatePurchaseOfferCommand(
        string Topic,
        string Description,
        int ModelId) : ICommand<Result<int>>;
}

using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Features.Common;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Application.Features.Offers.Commands.CreateOffer
{
    public sealed record CreateOfferCommand(
        string Topic,
        string Description,
        MoneyRequest Price,
        int ItemConditionId,
        int ColourId,
        int ProductCategoryId,
        int SizeId,
        int ModelId,
        int OfferTypeId,
        int Quantity = 1)
        : ICommand<Result<int>>;
}



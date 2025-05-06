using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Features.Common;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Application.Features.Offers.SaleOffers.Commands.CreateSaleOffer
{
    public sealed record CreateSaleOfferCommand(
        string Topic,
        string Description,
        MoneyRequest Price,
        int ItemConditionId,
        int ModelId,
        int SizeId,
        int DeliveryTypeId,
        int PaymentTypeId,
        List<int> ColourIds,
        int Quantity = 1)
        : ICommand<Result<int>>;
}



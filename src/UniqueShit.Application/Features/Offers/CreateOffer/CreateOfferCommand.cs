using UniqueShit.Application.Core.Messaging;

namespace UniqueShit.Application.Features.Offers.CreateOffer
{
    public sealed record CreateOfferCommand(
        string Topic,
        string Description,
        MoneyModel Price,
        int ItemConditionId,
        int ColourId,
        int ProductCategoryId,
        int SizeId,
        int ModelId,
        int OfferTypeId,
        int Quantity = 1)
        : ICommand<int>;

    public record MoneyModel(decimal Amount, string Currency);
}

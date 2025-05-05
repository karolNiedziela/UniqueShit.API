namespace UniqueShit.Application.Features.FavouriteOffers.Queries.GetFavouriteOffers
{
    public sealed record GetFavouriteOffersResponse(
        int Id,
        int OfferId,
        string OfferTopic);
}

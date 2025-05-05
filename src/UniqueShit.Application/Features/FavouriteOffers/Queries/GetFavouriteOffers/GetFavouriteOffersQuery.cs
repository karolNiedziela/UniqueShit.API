using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Queries;

namespace UniqueShit.Application.Features.FavouriteOffers.Queries.GetFavouriteOffers
{
    public sealed record GetFavouriteOffersQuery(int PageSize, int PageNumber) : IQuery<PagedList<GetFavouriteOffersResponse>>;
}

using UniqueShit.Application.Core.Authentication;
using UniqueShit.Application.Core.Extensions;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Domain.FavouriteOffers;

namespace UniqueShit.Application.Features.FavouriteOffers.Queries.GetFavouriteOffers
{
    public sealed class GetFavouriteOffersQueryHandler : IQueryHandler<GetFavouriteOffersQuery, PagedList<GetFavouriteOffersResponse>>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IDbContext _context;

        public GetFavouriteOffersQueryHandler(IUserIdentifierProvider userIdentifierProvider, IDbContext context)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _context = context;
        }

        public async Task<PagedList<GetFavouriteOffersResponse>> Handle(GetFavouriteOffersQuery request, CancellationToken cancellationToken)
        {
            var favouriteOffers = await _context
                .Set<FavouriteOffer>()
                .Where(x => x.AppUserId == _userIdentifierProvider.UserId)
                .Select(x => new GetFavouriteOffersResponse(
                    x.Id,
                    x.OfferId,
                    x.Offer.Topic.Value))
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            return favouriteOffers;
        }
    }
}

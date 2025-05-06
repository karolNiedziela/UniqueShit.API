using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Authentication;
using UniqueShit.Application.Core.Extensions;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Application.Features.Offers.Contracts.Responses;
using UniqueShit.Domain.Enumerations;
using UniqueShit.Domain.Offers;

namespace UniqueShit.Application.Features.Offers.SaleOffers.Queries.MySaleOffers
{
    public sealed class MySaleOffersQueryHandler : IQueryHandler<MySaleOffersQuery, PagedList<MySaleOffersResponse>>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        public MySaleOffersQueryHandler(IDbContext dbContext, IUserIdentifierProvider userIdentifierProvider)
        {
            _dbContext = dbContext;
            _userIdentifierProvider = userIdentifierProvider;
        }

        public async Task<PagedList<MySaleOffersResponse>> Handle(MySaleOffersQuery request, CancellationToken cancellationToken)
        {
            var myOffers = await _dbContext.Set<SaleOffer>()
                .AsNoTracking()
                .Where(x => x.AppUserId == _userIdentifierProvider.UserId)
                .OrderByDescending(x => x.CreatedOnUtc)
                .Select(x => new MySaleOffersResponse
                {
                    Id = x.Id,
                    Topic = x.Topic.Value,
                    Brand = new BrandResponse(x.Model.Brand.Id, x.Model.Brand.Name),
                    Model = new ModelResponse(x.Model.Id, x.Model.Name),
                    Price = new MoneyResponse(x.Price.Amount, x.Price.Currency),
                    ItemCondition = new EnumerationResponse(x.ItemConditionId, ItemCondition.FromValue(x.ItemConditionId).Value.Name),
                    Quantity = x.Quantity,
                })
                .PaginateAsync(PagedBase.DefaultPageNumber, PagedBase.DefaultPageSize, cancellationToken);

            return myOffers;
        }
    }
}

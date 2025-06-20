using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Extensions;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Application.Features.Offers.Contracts.Responses;
using UniqueShit.Domain.Enumerations;
using UniqueShit.Domain.Offers;
using UniqueShit.Domain.Offers.Enumerations;

namespace UniqueShit.Application.Features.Offers.SaleOffers.Queries.GetSaleOffers
{
    public sealed class GetSaleOffersQueryHandler : IQueryHandler<GetSaleOffersQuery, PagedList<GetSaleOffersResponse>>
    {
        private readonly IDbContext _dbContext;

        public GetSaleOffersQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedList<GetSaleOffersResponse>> Handle(GetSaleOffersQuery request, CancellationToken cancellationToken)
        {
            var offersQuery = _dbContext.Set<SaleOffer>()
                .AsNoTracking()
                .Where(x => x.OfferStateId == OfferState.Active.Id)
                .AsQueryable();

            if (request.ItemConditionId.HasValue)
            {
                offersQuery = offersQuery.Where(x => x.ItemConditionId == request.ItemConditionId.Value);
            }

            if (request.SizeId.HasValue)
            {
                offersQuery = offersQuery.Where(x => x.SizeId == request.SizeId.Value);
            }

            if (request.BrandId.HasValue)
            {
                offersQuery = offersQuery
                    .Where(x => x.Model.BrandId == request.BrandId.Value);
            }

            if (request.ProductCategoryId.HasValue)
            {
                offersQuery = offersQuery
                    .Where(x => x.Model.ProductCategoryId == request.ProductCategoryId.Value);
            }

            if (request.ModelId.HasValue)
            {
                offersQuery = offersQuery
                    .Where(x => x.ModelId == request.ModelId.Value);
            }

            if (request.UserId.HasValue)
            {
                offersQuery = offersQuery
                    .Where(x => x.AppUserId == request.UserId.Value);
            }

            offersQuery = ApplyPriceRangeFilter(offersQuery, request);

            var offers = await offersQuery
                .OrderByDescending(x => x.CreatedOnUtc)
                .Select(x => new GetSaleOffersResponse
                {
                    Id = x.Id,
                    Topic = x.Topic.Value,
                    Brand = new BrandResponse(x.Model.Brand.Id, x.Model.Brand.Name),
                    Model = new ModelResponse(x.Model.Id, x.Model.Name),
                    Price = new MoneyResponse(x.Price.Amount, x.Price.Currency),
                    ItemCondition = new EnumerationResponse(x.ItemConditionId, ItemCondition.FromValue(x.ItemConditionId).Value.Name),
                    Quantity = x.Quantity
                })
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            return offers;
        }

        private IQueryable<SaleOffer> ApplyPriceRangeFilter(IQueryable<SaleOffer> offersQuery, GetSaleOffersQuery request)
        {
            if (request.MinimalPrice.HasValue)
            {
                offersQuery = offersQuery.Where(x => x.Price.Amount >= request.MinimalPrice.Value);
            }

            if (request.MaximumPrice.HasValue)
            {
                offersQuery = offersQuery.Where(x => x.Price.Amount <= request.MaximumPrice.Value);
            }

            return offersQuery;
        }
    }
}

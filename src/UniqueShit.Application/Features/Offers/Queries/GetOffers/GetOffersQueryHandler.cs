using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Extensions;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Application.Features.Offers.Contracts.Responses;
using UniqueShit.Domain.Enumerations;
using UniqueShit.Domain.Offers;

namespace UniqueShit.Application.Features.Offers.Queries.GetOffers
{
    public sealed class GetOffersQueryHandler : IQueryHandler<GetOffersQuery, PagedList<GetOffersResponse>>
    {
        private readonly IDbContext _dbContext;

        public GetOffersQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedList<GetOffersResponse>> Handle(GetOffersQuery request, CancellationToken cancellationToken)
        {
            var offerType = OfferType.FromValue(request.OfferTypeId);
            if (offerType.IsFailure)
            {
                return PagedList<GetOffersResponse>.Empty();
            }

            var offersQuery = _dbContext.Set<Offer>()
                .AsNoTracking()
                .Where(x => x.OfferTypeId == offerType.Value.Id && x.OfferStateId == OfferState.Active.Id)
                .AsQueryable();

            if (offersQuery is null)
            {
                return PagedList<GetOffersResponse>.Empty();
            }

            if (request.ItemConditionId.HasValue)
            {
                offersQuery = offersQuery.Where(x => x.ItemConditionId == request.ItemConditionId.Value);
            }

            if (request.SizeId.HasValue)
            {
                offersQuery = offersQuery.Where(x => x.SizeId == request.SizeId.Value);
            }

            if (request.ManufacturerId.HasValue)
            {
                offersQuery = offersQuery
                    .Where(x => x.ManufacturerId == request.ManufacturerId.Value);
            }

            if (request.ProductCategoryId.HasValue)
            {
                offersQuery = offersQuery
                    .Where(x => x.ProductCategoryId == request.ProductCategoryId.Value);
            }

            var offers = await offersQuery
                .OrderByDescending(x => x.CreatedOnUtc)
                .Select(x => new GetOffersResponse
                {
                    Id = x.Id,
                    Topic = x.Topic.Value,
                    Manufacturer = new OfferManufacturerResponse(x.Manufacturer.Id, x.Manufacturer.Name),
                    Price = new MoneyResponse(x.Price.Amount, x.Price.Currency),
                    ItemCondition = new EnumerationResponse(x.ItemConditionId, ItemCondition.FromValue(x.ItemConditionId).Value.Name),
                    Colours = x.Colours.Select(c => new EnumerationResponse(c.Id, c.Name)).ToList(),
                    Quantity = x.Quantity,
                })
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            return offers;
        }
    }
}

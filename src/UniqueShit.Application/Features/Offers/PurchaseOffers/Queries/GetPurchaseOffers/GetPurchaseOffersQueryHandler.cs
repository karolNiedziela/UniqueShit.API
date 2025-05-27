using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Extensions;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Queries;
using UniqueShit.Application.Features.Offers.Contracts.Responses;
using UniqueShit.Domain.Offers;
using UniqueShit.Domain.Offers.Enumerations;

namespace UniqueShit.Application.Features.Offers.PurchaseOffers.Queries.GetPurchaseOffers
{
    public sealed class GetPurchaseOffersQueryHandler : IQueryHandler<GetPurchaseOffersQuery, PagedList<GetPurchaseOffersResponse>>
    {
        private readonly IDbContext _dbContext;

        public GetPurchaseOffersQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedList<GetPurchaseOffersResponse>> Handle(GetPurchaseOffersQuery request, CancellationToken cancellationToken)
        {
            var purchaseOffersQuery = _dbContext.Set<PurchaseOffer>()
               .AsNoTracking()
               .Where(x => x.OfferStateId == OfferState.Active.Id)
               .AsQueryable();

            if (request.BrandId.HasValue)
            {
                purchaseOffersQuery = purchaseOffersQuery
                    .Where(x => x.Model.BrandId == request.BrandId.Value);
            }

            if (request.ProductCategoryId.HasValue)
            {
                purchaseOffersQuery = purchaseOffersQuery
                    .Where(x => x.Model.ProductCategoryId == request.ProductCategoryId.Value);
            }

            if (request.ModelId.HasValue)
            {
                purchaseOffersQuery = purchaseOffersQuery
                    .Where(x => x.ModelId == request.ModelId.Value);
            }

            var purchaseOffers = await purchaseOffersQuery
               .OrderByDescending(x => x.CreatedOnUtc)
               .Select(x => new GetPurchaseOffersResponse(
                   x.Id,
                   x.Topic.Value,
                   x.Description.Value,
                   new BrandResponse(x.Model.Brand.Id, x.Model.Brand.Name),
                   new ModelResponse(x.Model.Id, x.Model.Name))
               )
               .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            return purchaseOffers;
        }
    }
}

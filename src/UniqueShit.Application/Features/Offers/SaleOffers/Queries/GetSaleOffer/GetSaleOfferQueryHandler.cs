using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Application.Features.Offers.Contracts.Responses;
using UniqueShit.Application.Features.Sizes.Queries.GetSizes;
using UniqueShit.Domain.Enumerations;
using UniqueShit.Domain.Offers;
using UniqueShit.Domain.Offers.Enumerations;

namespace UniqueShit.Application.Features.Offers.SaleOffers.Queries.GetSaleOffer
{
    public sealed class GetSaleOfferQueryHandler : IQueryHandler<GetSaleOfferQuery, GetSaleOfferResponse?>
    {
        private readonly IDbContext _dbContext;

        public GetSaleOfferQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetSaleOfferResponse?> Handle(GetSaleOfferQuery request, CancellationToken cancellationToken)
        {
            var offer = await _dbContext.Set<SaleOffer>()
                .AsNoTracking()
                .Select(x => new GetSaleOfferResponse
                {
                    Id = x.Id,
                    Topic = x.Topic.Value,
                    Description = x.Description.Value,
                    Model = new ModelDetailsResponse(x.Model.Id, x.Model.Name, new BrandResponse(x.Model.BrandId, x.Model.Brand.Name), new EnumerationResponse(x.Model.ProductCategoryId, x.Model.ProductCategory.Name)),
                    Price = new MoneyResponse(x.Price.Amount, x.Price.Currency),
                    Size = new SizeResponse(x.SizeId, x.Size.Value),
                    ItemCondition = new EnumerationResponse(x.ItemConditionId, ItemCondition.FromValue(x.ItemConditionId).Value.Name),
                    DeliveryType = new EnumerationResponse(x.DeliveryTypeId, DeliveryType.FromValue(x.DeliveryTypeId).Value.Name),
                    PaymentType = new EnumerationResponse(x.PaymentTypeId, PaymentType.FromValue(x.PaymentTypeId).Value.Name),
                    Quantity = x.Quantity,
                    User = new UserResponse(x.AppUserId, x.AppUser.DisplayName)
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return offer;
        }
    }
}

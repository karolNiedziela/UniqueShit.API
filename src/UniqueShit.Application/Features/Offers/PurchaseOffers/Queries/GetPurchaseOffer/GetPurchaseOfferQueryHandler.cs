using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Features.Offers.Contracts.Responses;
using UniqueShit.Domain.Offers;

namespace UniqueShit.Application.Features.Offers.PurchaseOffers.Queries.GetPurchaseOffer
{
    public sealed class GetPurchaseOfferQueryHandler : IQueryHandler<GetPurchaseOfferQuery, GetPurchaseOfferResponse?>
    {
        private readonly IDbContext _dbContext;

        public GetPurchaseOfferQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetPurchaseOfferResponse?> Handle(GetPurchaseOfferQuery request, CancellationToken cancellationToken)
        {
            var purchaseOffer = await _dbContext.Set<PurchaseOffer>()
                .AsNoTracking()
                .Select(x => new GetPurchaseOfferResponse
                {
                    Id = x.Id,
                    Topic = x.Topic.Value,
                    Description = x.Description.Value,
                    Model = new ModelResponse(x.ModelId, x.Model.Name)
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return purchaseOffer;
        }
    }
}

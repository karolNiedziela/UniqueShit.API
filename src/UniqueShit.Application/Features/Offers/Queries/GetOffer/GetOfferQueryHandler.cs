using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Application.Features.Offers.Contracts.Responses;
using UniqueShit.Domain.Enumerations;
using UniqueShit.Domain.Offers;

namespace UniqueShit.Application.Features.Offers.Queries.GetOffer
{
    public sealed class GetOfferQueryHandler : IQueryHandler<GetOfferQuery, GetOfferResponse?>
    {
        private readonly IDbContext _dbContext;

        public GetOfferQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetOfferResponse?> Handle(GetOfferQuery request, CancellationToken cancellationToken)
        {
            var offer = await _dbContext.Set<Offer>()
                .AsNoTracking()
                .Select(x => new GetOfferResponse
                {
                    Id = x.Id,
                    Topic = x.Topic.Value,
                    Description = x.Description.Value,
                    Manufacturer = new OfferManufacturerResponse(x.Manufacturer.Id, x.Manufacturer.Name),
                    Price = new MoneyResponse(x.Price.Amount, x.Price.Currency),
                    ItemCondition = new EnumerationResponse(x.ItemConditionId, ItemCondition.FromValue(x.ItemConditionId).Value.Name),
                    Colours = x.Colours.Select(c => new EnumerationResponse(c.Id, c.Name)).ToList(),
                    Quantity = x.Quantity,
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return offer;
        }
    }
}

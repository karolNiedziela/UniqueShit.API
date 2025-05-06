using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Authentication;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Core.Errors;
using UniqueShit.Domain.Core.Primitives.Results;
using UniqueShit.Domain.Offers;

namespace UniqueShit.Application.Features.Offers.SaleOffers.Commands.DeleteSaleOffer
{
    public sealed class DeleteSaleOfferCommandHandler : ICommandHandler<DeleteSaleOfferCommand, Result>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        public DeleteSaleOfferCommandHandler(IDbContext dbContext, IUserIdentifierProvider userIdentifierProvider)
        {
            _dbContext = dbContext;
            _userIdentifierProvider = userIdentifierProvider;
        }

        public async Task<Result> Handle(DeleteSaleOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await _dbContext.Set<SaleOffer>()
                .Where(x => x.Id == request.Id)
                .Select(x => new
                {
                    x.Id,
                    x.AppUserId
                })
                .FirstOrDefaultAsync(cancellationToken);
            if (offer is null)
            {
                return DomainErrors.Offer.OfferNotFound;
            }

            if (offer.AppUserId != _userIdentifierProvider.UserId)
            {
                return DomainErrors.Offer.NoPrivilegesToRemove;
            }

            await _dbContext
               .Set<SaleOffer>()
               .Where(t => t.Id == offer.Id)
               .ExecuteDeleteAsync(cancellationToken);

            return Result.Success();
        }
    }
}

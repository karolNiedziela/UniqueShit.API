using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Authentication;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Core.Errors;
using UniqueShit.Domain.Core.Primitives.Results;
using UniqueShit.Domain.Offers;

namespace UniqueShit.Application.Features.Offers.Commands.DeleteOffer
{
    public sealed class DeleteOfferCommandHandler : ICommandHandler<DeleteOfferCommand, Result>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        public DeleteOfferCommandHandler(IDbContext dbContext, IUserIdentifierProvider userIdentifierProvider)
        {
            _dbContext = dbContext;
            _userIdentifierProvider = userIdentifierProvider;
        }

        public async Task<Result> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await _dbContext.Set<Offer>()
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
               .Set<Offer>()
               .Where(t => t.Id == offer.Id)
               .ExecuteDeleteAsync(cancellationToken);

            return Result.Success();
        }
    }
}

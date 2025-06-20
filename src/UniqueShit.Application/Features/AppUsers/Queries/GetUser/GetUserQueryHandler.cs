using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.ValueObjects;

namespace UniqueShit.Application.Features.AppUsers.Queries.GetUser
{
    public sealed class GetUserQueryHandler : IQueryHandler<GetUserQuery, GetUserResponse?>
    {
        private readonly IDbContext _dbContext;

        public GetUserQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetUserResponse?> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var appUser = await _dbContext.Set<AppUser>()
                .AsNoTracking()
                .Where(x => x.ADObjectId == request.UserId)
                .Select(x => new GetUserResponse(
                    x.ADObjectId,
                    x.Email,
                    x.DisplayName,
                    x.PhoneNumber!,
                    x.AboutMe,
                    x.City))
                .FirstOrDefaultAsync(cancellationToken);

            return appUser;
        }
    }
}

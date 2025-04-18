using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Infrastructure.Persistence.Repositories
{
    internal sealed class AppUserRepository : IAppUserRepository
    {
        private readonly DbSet<AppUser> _users;

        public AppUserRepository(IDbContext dbContext)
        {
            _users = dbContext.Set<AppUser>();
        }

        public async Task<AppUser?> GetByEmailAsync(string email)
            => await _users.FirstOrDefaultAsync(x => x.Email == email);

        public void Add(AppUser appUser)
           => _users.Add(appUser);
    }
}

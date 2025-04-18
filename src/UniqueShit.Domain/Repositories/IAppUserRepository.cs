using UniqueShit.Domain.Enitities;

namespace UniqueShit.Domain.Repositories
{
    public interface IAppUserRepository
    {
        Task<AppUser?> GetByEmailAsync(string email);

        void Add(AppUser appUser);
    }
}

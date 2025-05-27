using UniqueShit.Domain.Enitities;

namespace UniqueShit.Domain.Repositories
{
    public interface IAppUserRepository
    {
        Task<AppUser?> GetByEmailAsync(string email);

        Task<AppUser?> GetById(Guid id);

        void Add(AppUser appUser);
    }
}

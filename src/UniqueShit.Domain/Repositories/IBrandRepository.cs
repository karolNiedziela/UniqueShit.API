using UniqueShit.Domain.Enitities;

namespace UniqueShit.Domain.Repositories
{
    public interface IBrandRepository
    {
        Task<Brand?> GetAsync(int id);
    }
}

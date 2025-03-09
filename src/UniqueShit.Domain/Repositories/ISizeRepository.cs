using UniqueShit.Domain.Enitities;

namespace UniqueShit.Domain.Repositories
{
    public interface ISizeRepository
    {
        Task<Size?> GetAsync(int id, int productCategoryId);
    }
}

using UniqueShit.Domain.Enitities;

namespace UniqueShit.Domain.Repositories
{
    public interface IModelRepository
    {
        Task<Model?> GetAsync(int id);
    }
}

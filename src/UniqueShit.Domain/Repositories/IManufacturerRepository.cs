using UniqueShit.Domain.Enitities;

namespace UniqueShit.Domain.Repositories
{
    public interface IManufacturerRepository
    {
        Task<Manufacturer?> GetAsync(int id);
    }
}

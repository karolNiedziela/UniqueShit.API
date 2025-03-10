using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Infrastructure.Persistence.Repositories
{
    internal sealed class ManufacturerRepository : IManufacturerRepository
    {
        private readonly DbSet<Manufacturer> _manufacturerSet;

        public ManufacturerRepository(IDbContext context)
        {
            _manufacturerSet = context.Set<Manufacturer>();
        }

        public async Task<Manufacturer?> GetAsync(int id)
            => await _manufacturerSet.FirstOrDefaultAsync(x => x.Id == id);
    }
}

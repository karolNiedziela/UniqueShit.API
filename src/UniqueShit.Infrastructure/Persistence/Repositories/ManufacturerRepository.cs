using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Infrastructure.Persistence.Repositories
{
    internal sealed class ManufacturerRepository : IBrandRepository
    {
        private readonly DbSet<Brand> _manufacturerSet;

        public ManufacturerRepository(IDbContext context)
        {
            _manufacturerSet = context.Set<Brand>();
        }

        public async Task<Brand?> GetAsync(int id)
            => await _manufacturerSet.FirstOrDefaultAsync(x => x.Id == id);
    }
}

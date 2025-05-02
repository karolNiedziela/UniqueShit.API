using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Infrastructure.Persistence.Repositories
{
    internal sealed class ModelRepository : IModelRepository
    {
        private readonly DbSet<Model> _models;

        public ModelRepository(IDbContext dbContext)
        {
            _models = dbContext.Set<Model>();
        }

        public async Task<Model?> GetAsync(int id)
            => await _models.FirstOrDefaultAsync(x => x.Id == id);

        public void Add(Model model)
            => _models.Add(model);
    }
}

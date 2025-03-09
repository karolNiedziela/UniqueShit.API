using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Infrastructure.Persistence.Repositories
{
    internal sealed class SizeRepository : ISizeRepository
    {
        private readonly DbSet<Size> _sizes;

        public SizeRepository(IDbContext dbContext)
        {
            _sizes = dbContext.Set<Size>();
        }

        public async Task<Size?> GetAsync(int id, int productCategoryId)
            => await _sizes.FirstOrDefaultAsync(x => x.Id == id && x.ProductCategoryId == productCategoryId);
    }
}

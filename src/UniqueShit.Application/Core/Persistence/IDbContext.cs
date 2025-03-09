using Microsoft.EntityFrameworkCore;

namespace UniqueShit.Application.Core.Persistence
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}

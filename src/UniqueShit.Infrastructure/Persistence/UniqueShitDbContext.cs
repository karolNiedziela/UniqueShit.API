using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using UniqueShit.Application.Core.Persistence;

namespace UniqueShit.Infrastructure.Persistence
{
    public sealed class UniqueShitDbContext : DbContext, IDbContext, IUnitOfWork
    {

        public UniqueShitDbContext(DbContextOptions options) : base(options)
        {            
        }

        public new DbSet<TEntity> Set<TEntity>()
            where TEntity : class
            => base.Set<TEntity>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await base.SaveChangesAsync(cancellationToken);

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
            => Database.BeginTransactionAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}

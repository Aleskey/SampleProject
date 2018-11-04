using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Common.Interfaces
{
    public interface IDbContext
    {
        IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class;

        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.Common.Interfaces;
using SampleProject.DataAccess.Entities;

namespace SampleProject.DataAccess
{
    public class RateDbContext : DbContext, IDbContext
    {
        public RateDbContext(DbContextOptions<RateDbContext> options)
            : base(options)
        {
        }

        public DbSet<Rate> Rates { get; set; }
        public IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>().AsQueryable();
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
           this.Set<TEntity>().AddRange(entities);
        }

        public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            await base.Set<TEntity>().AddRangeAsync(entities);
        }
    }
}

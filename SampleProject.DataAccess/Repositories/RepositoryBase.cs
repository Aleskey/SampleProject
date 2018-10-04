using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleProject.Common;

namespace SampleProject.DataAccess.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class 
    {
        protected RateDbContext RateDbContext { get; set; }

        public RepositoryBase(RateDbContext context)
        {
            this.RateDbContext = context;
        }

        public IQueryable<T> All => RateDbContext.Set<T>();

        public async Task<IEnumerable<T>> InsertRangeAsync(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            await RateDbContext.Set<T>().AddRangeAsync(entities);

            return entities;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await RateDbContext.SaveChangesAsync();
        }
    }
}
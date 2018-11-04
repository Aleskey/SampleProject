using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.Common.Interfaces;

namespace SampleProject.DataAccess.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T>
        where T : class 
    {
        private DbSet<T> dbSet { get; set; }

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            this.dbSet = unitOfWork.Context.Set<T>();
        }

        public IQueryable<T> GetAll => dbSet.AsQueryable();

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public async void AddRangeAsync(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }
    }
}
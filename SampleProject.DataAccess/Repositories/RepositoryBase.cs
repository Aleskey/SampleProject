using System.Linq;
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
    }
}
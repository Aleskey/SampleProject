using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject.Common
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All { get; }
        Task<IEnumerable<T>> InsertRangeAsync(IEnumerable<T> entities);
        Task<int> SaveChangesAsync();
    }
}
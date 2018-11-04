using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll { get; }

        Task<ICollection<T>> GetAllAsync();

        void AddRange(IEnumerable<T> entities);

        void AddRangeAsync(IEnumerable<T> entities);
    }
}
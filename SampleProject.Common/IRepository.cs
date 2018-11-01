using System.Linq;

namespace SampleProject.Common
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All { get; }
    }
}
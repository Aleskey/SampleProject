using System.Threading.Tasks;

namespace SampleProject.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IDbContext Context { get; }

        IRepository<T> GetRepository<T>() where T : class;

        Task<int> SaveChangesAsync();

        void SaveChanges();
    }
}
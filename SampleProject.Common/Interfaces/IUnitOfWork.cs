using System.Threading.Tasks;

namespace SampleProject.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IDbContext Context { get; }

        Task<int> SaveChangesAsync();

        void SaveChanges();
    }
}
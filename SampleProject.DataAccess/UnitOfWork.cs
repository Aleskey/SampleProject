using System;
using System.Threading.Tasks;
using SampleProject.Common.Interfaces;

namespace SampleProject.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool disposed;

        public UnitOfWork(IDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IDbContext Context { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                Context.Dispose();
            }

            disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
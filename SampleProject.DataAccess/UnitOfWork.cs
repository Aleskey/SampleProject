using System;
using System.Threading.Tasks;
using SampleProject.Common.Interfaces;
using SampleProject.DataAccess.Repositories;

namespace SampleProject.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRepositoryFactory repositoryFactory;

        public UnitOfWork(IDbContext context, IRepositoryFactory repositoryFactory)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            this.repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        public IDbContext Context { get; }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return repositoryFactory.Create<T>(this);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
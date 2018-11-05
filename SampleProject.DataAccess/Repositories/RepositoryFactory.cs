using System;
using SampleProject.Common.Interfaces;

namespace SampleProject.DataAccess.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepository<T> Create<T>(IUnitOfWork unitOfWork) where T : class
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), unitOfWork);
            return (IRepository<T>)repositoryInstance;
        }
    }
}
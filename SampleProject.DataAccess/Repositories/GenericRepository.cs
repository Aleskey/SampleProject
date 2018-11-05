using System;
using System.Collections.Generic;
using System.Linq;
using SampleProject.Common.Interfaces;
using SampleProject.DataAccess.Entities;

namespace SampleProject.DataAccess.Repositories
{
    public class GenericRepository<T> : IRepository<T>
        where T : Entity
    {
        private readonly IUnitOfWork unitOfWork;

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public IQueryable<T> GetAll => this.unitOfWork.Context.GetQueryable<T>();

        public void AddRange(IEnumerable<T> entities)
        {
            unitOfWork.Context.AddRange(entities);
        }

        public async void AddRangeAsync(IEnumerable<T> entities)
        {
            await unitOfWork.Context.AddRangeAsync(entities);
        }
    }
}
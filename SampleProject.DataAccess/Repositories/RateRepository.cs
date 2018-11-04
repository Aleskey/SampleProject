using SampleProject.Common.Interfaces;
using SampleProject.DataAccess.Entities;

namespace SampleProject.DataAccess.Repositories
{
    public class RateRepository : GenericRepository<Rate>
    {
        public RateRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
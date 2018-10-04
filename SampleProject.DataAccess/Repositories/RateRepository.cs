using SampleProject.DataAccess.Entities;

namespace SampleProject.DataAccess.Repositories
{
    public class RateRepository : RepositoryBase<Rate>
    {
        public RateRepository(RateDbContext context) : base(context)
        {
        }
    }
}
using Microsoft.EntityFrameworkCore;
using SampleProject.Common.Interfaces;
using SampleProject.DataAccess.Entities;

namespace SampleProject.DataAccess
{
    public class RateDbContext : DbContext, IDbContext
    {
        public RateDbContext(DbContextOptions<RateDbContext> options)
            : base(options)
        {
        }

        public DbSet<Rate> Rates { get; set; }
    }
}

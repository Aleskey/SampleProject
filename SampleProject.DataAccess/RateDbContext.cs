using Microsoft.EntityFrameworkCore;
using System;
using SampleProject.DataAccess.Entities;

namespace SampleProject.DataAccess
{
    public class RateDbContext : DbContext
    {
        public RateDbContext(DbContextOptions<RateDbContext> options)
            : base(options)
        {
        }

        public DbSet<Rate> Rates { get; set; }
    }
}

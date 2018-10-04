using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SampleProject.Api.Tests.Extensions;
using SampleProject.DataAccess;

namespace SampleProject.Api.Tests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration, IHostingEnvironment hostingEnvironment) : base(configuration, hostingEnvironment)
        {
            hostingEnvironment.ApplicationName = "SampleProject.Api";
        }

        public override void SeedDataBase(RateDbContext context)
        {
            context.EnsureSeedTestDataForContext();
        }
    }
}
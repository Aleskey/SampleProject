using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace SampleProject.Api.Tests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration, IHostingEnvironment hostingEnvironment) : base(configuration, hostingEnvironment)
        {
            hostingEnvironment.ApplicationName = "SampleProject.Api";
        }
    }
}
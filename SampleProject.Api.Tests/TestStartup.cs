using Microsoft.AspNetCore.Hosting;

namespace SampleProject.Api.Tests
{
    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment hostingEnvironment)
        {
            hostingEnvironment.ApplicationName = "SampleProject.Api";
        }
    }
}
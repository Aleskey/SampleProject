using Microsoft.AspNetCore.Hosting;

namespace SampleProject.Api.Extensions
{
    public static class HostingEnvironmentExtensions
    {
        public static bool IsTesting(this IHostingEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsEnvironment("Testing");
        }
    }
}
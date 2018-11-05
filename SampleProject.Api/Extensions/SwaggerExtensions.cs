using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using SampleProject.Common;

namespace SampleProject.Api.Extensions
{
    public static class SwaggerExtensions
    {
        private const string SwaggerRoutePrefix = "swagger";

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwagger();

            applicationBuilder.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint($"../{SwaggerRoutePrefix}/v1/swagger.json", StringConstants.ApiName);
                opt.RoutePrefix = SwaggerRoutePrefix;
            });

            var options = new RewriteOptions();
            options.AddRedirect("^$", SwaggerRoutePrefix);
            return applicationBuilder.UseRewriter(options);
        }
    }
}
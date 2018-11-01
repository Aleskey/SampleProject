using System;
using Microsoft.AspNetCore.Builder;

namespace SampleProject.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwagger();

            applicationBuilder.UseSwaggerUI(opt =>
            {
                string basePath = Environment.GetEnvironmentVariable("ASPNETCORE_APPL_PATH");
                opt.SwaggerEndpoint($"{basePath}/swagger/v1/swagger.json", "SampleProject API");
                opt.RoutePrefix = "";
            });

            return applicationBuilder;
        }
    }
}
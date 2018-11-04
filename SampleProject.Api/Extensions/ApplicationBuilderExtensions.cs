using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;

namespace SampleProject.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwagger();

            applicationBuilder.UseSwaggerUI(opt => opt.SwaggerEndpoint("../swagger/v1/swagger.json", "SampleProject API"));

            var options = new RewriteOptions();
            options.AddRedirect("^$", "swagger");
            return applicationBuilder.UseRewriter(options);
        }
    }
}
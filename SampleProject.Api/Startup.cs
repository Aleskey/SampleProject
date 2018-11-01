using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleProject.Api.Extensions;
using SampleProject.Core.Factories;
using SampleProject.DataAccess;

namespace SampleProject.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment CurrentEnvironment { get; }

        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServices();

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddXmlSerializerFormatters();

            services.RegisterSwagger();
        }

        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            RateDbContext rateDbContext,
            DataProviderFactory dataProviderFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            if (CurrentEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            rateDbContext.EnsureSeedDataForContext(dataProviderFactory);

            app.ConfigureSwagger();

            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

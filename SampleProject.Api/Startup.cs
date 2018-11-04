using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleProject.Api.Extensions;
using SampleProject.Common.Interfaces;
using SampleProject.DataAccess;
using SampleProject.DataAccess.DataProvider;
using SampleProject.DataAccess.Entities;

namespace SampleProject.Api
{
    public class Startup
    {
        public void ConfigureServices(
            IServiceCollection services)
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
            IConfiguration configuration,
            IHostingEnvironment hostingEnvironment,
            ILoggerFactory loggerFactory,
            IDataProviderFactory dataProviderFactory,
            IUnitOfWork unitOfWork,
            IRepository<Rate> rateRepository)
        {
            loggerFactory.AddConsole(configuration.GetSection("Logging"));

            if (hostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            rateRepository.EnsureSeedData(dataProviderFactory, unitOfWork);

            app.ConfigureSwagger();
            app.UseStatusCodePages();
            app.UseMvc();
        }
    }
}

using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleProject.Api.Extensions;
using SampleProject.DataAccess;
using Swashbuckle.AspNetCore.Swagger;

namespace SampleProject.Api
{
    /// <summary>
    /// Represents entry point and configure App
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the current environment.
        /// </summary>
        public IHostingEnvironment CurrentEnvironment { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServices(CurrentEnvironment);

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddXmlSerializerFormatters();

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Info
                {
                    Title = "SampleProject API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Version = "v1",
                    Contact = new Contact
                    {
                        Name = "Alexey Karpov",
                        Email = "aleksey.karpov1982@gmail.com"
                    }
                });

                var path = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                opt.IncludeXmlComments(path);
            });
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="rateDbContext">The rate database context.</param>
        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            RateDbContext rateDbContext)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (CurrentEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            SeedDataBase(rateDbContext);

            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleProject API");
                opt.RoutePrefix = "swagger";
            });

            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public virtual void SeedDataBase(RateDbContext context)
        {
            context.EnsureSeedDataForContext();
        }
    }
}

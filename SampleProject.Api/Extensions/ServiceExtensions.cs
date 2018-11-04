using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Common.Interfaces;
using SampleProject.Core.Interfaces;
using SampleProject.Core.Services;
using SampleProject.DataAccess;
using SampleProject.DataAccess.DataProvider;
using SampleProject.DataAccess.Entities;
using SampleProject.DataAccess.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace SampleProject.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddDbContext<IDbContext, RateDbContext>(opt => opt.UseInMemoryDatabase("SampleDatabase"));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IRepository<Rate>, RateRepository>();
            services.AddTransient<IRateCalculationService, RateCalculationService>();
            services.AddTransient<IDataProviderFactory, DataProviderFactory>();

            return services;
        }

        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(opt =>
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
    }
}
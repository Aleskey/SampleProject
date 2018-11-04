using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Common;
using SampleProject.Core;
using SampleProject.Core.Factories;
using SampleProject.Core.Interfaces;
using SampleProject.DataAccess;
using SampleProject.DataAccess.Entities;
using SampleProject.DataAccess.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace SampleProject.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddDbContext<RateDbContext>(opt => opt.UseInMemoryDatabase("SampleDatabase"));

            services.AddTransient<IRepository<Rate>, RateRepository>();
            services.AddTransient<IRateCalculationAction, RateCalculationAction>();
            services.AddTransient<DataProviderFactory, DataProviderFactory>();

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
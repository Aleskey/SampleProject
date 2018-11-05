using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Common;
using SampleProject.Common.Interfaces;
using SampleProject.Core.Interfaces;
using SampleProject.Core.Services;
using SampleProject.DataAccess;
using SampleProject.DataAccess.DataProvider;
using SampleProject.DataAccess.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace SampleProject.Api.Extensions
{
    public static class ServiceExtensions
    {
        private const string ApiVersion = "v1";

        private const string ApiDescription = "API that retrieves rate for given date range";

        private const string ContactName = "Alexey Karpov";

        private const string ContactEmail = "aleksey.karpov1982@gmail.com";

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddDbContext<IDbContext, RateDbContext>(opt => opt.UseInMemoryDatabase("SampleDatabase"));

            services.AddTransient<IRepositoryFactory, RepositoryFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IRateService, RateFindService>();
            services.AddTransient<IDataProviderFactory, DataProviderFactory>();

            return services;
        }

        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc(ApiVersion, new Info
                {
                    Title = StringConstants.ApiName,
                    Description = ApiDescription,
                    Version = ApiVersion,
                    Contact = new Contact
                    {
                        Name = ContactName,
                        Email = ContactEmail
                    }
                });

                var path = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                opt.IncludeXmlComments(path);
            });
        }
    }
}
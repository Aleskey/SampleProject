using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Common;
using SampleProject.Core;
using SampleProject.Core.Interfaces;
using SampleProject.DataAccess;
using SampleProject.DataAccess.Entities;
using SampleProject.DataAccess.Repositories;

namespace SampleProject.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services,
            IHostingEnvironment hostingEnvironment)
        {
            var dbName = hostingEnvironment.IsTesting() ? "TestingDBContext" : "DefaultContext";

            services.AddDbContext<RateDbContext>(opt => opt.UseInMemoryDatabase(dbName));

            services.AddTransient<IRepository<Rate>, RateRepository>();
            services.AddTransient<IRateCalculationAction, RateCalculationAction>();

            return services;
        }
    }
}
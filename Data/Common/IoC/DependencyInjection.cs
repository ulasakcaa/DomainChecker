using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using Data.Common;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Application;
using Microsoft.Extensions.Configuration;

namespace Test.Data.Common.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IDomainCheckDbContext, DomainCheckDbContext>();          

            return services;
        }
    }
}

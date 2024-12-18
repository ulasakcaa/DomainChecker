using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using Shared.Common.Interface;

namespace Business.Common.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            services.RegisterAssemblyPublicNonGenericClasses(AppDomain.CurrentDomain.GetAssemblies())
                .Where(x => x.GetInterfaces().Contains(typeof(IBaseBusiness)))  //optional
                .IgnoreThisInterface<IBaseBusiness>()     //optional
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);


            services.AddScoped<IDomainCheckerBusiness, DomainCheckerBusiness>();


            return services;
        }
    }
}

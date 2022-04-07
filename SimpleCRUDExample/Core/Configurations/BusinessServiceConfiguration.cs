using Business.Service.Engines;
using Business.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleCRUDExample.Core.Configurations
{
    public static class BusinessServiceConfiguration
    {
        public static IServiceCollection ConfigureBusinessEngines(this IServiceCollection services)
        {
            
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}

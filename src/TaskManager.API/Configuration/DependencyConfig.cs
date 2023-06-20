using FinancialControl.Core.Interfaces;
using FinancialControl.Infra.Data;
using TaskManager.Core.Interfaces;
using TaskManager.Domain.Notifications;

namespace TaskManager.API.Configuration
{
    public static class DependencyConfig
    {
        public static IServiceCollection AddDependencyConfig(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<INotifiable, Notifiable>();
            return services;
        }
    }
}

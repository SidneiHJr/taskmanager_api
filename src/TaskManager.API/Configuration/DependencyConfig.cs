using FinancialControl.Core.Interfaces;
using FinancialControl.Infra.Data;
using TaskManager.Core.Interfaces;
using TaskManager.Domain.Notifications;
using TaskManager.Domain.Services;
using TaskManager.Infra.Data.Transactions;

namespace TaskManager.API.Configuration
{
    public static class DependencyConfig
    {
        public static IServiceCollection AddDependencyConfig(this IServiceCollection services)
        {
            services.AddScoped(typeof(IService<>), typeof(Service<>));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<INotifiable, Notifiable>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}

using Application.Interfaces.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;

namespace Persistence.Extensions
{
    public static class DatabaseServicesExtension
    {
        public static void ImplementApplicationDatabaseInterfaces(this IServiceCollection services) {
            // Add Auth Repository
            services.AddTransient<IAuthRepository, AuthRepository>();

            // Location Repository for getting anything relating to place
            services.AddTransient<ILocationRepository, LocationRepository>();

            // A central repostory like the datacontext
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Repository for users related actions
            services.AddTransient<IUserRepository, UserRepository>();

            // Repository for users related actions
        }
    }
}
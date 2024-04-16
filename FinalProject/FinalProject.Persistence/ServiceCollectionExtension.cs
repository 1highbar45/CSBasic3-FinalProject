using FinalProject.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinalProject.Persistence
{
    public static class ServiceCollectionExtension
    {
        public static void AddRepositoryUnitOfWork(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        }

        public static void AddEfCore(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var assembly = typeof(ApplicationDbContext).Assembly.GetName().Name;
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration["DefaultConnection"], b => b.MigrationsAssembly(assembly)));
        }
    }
}

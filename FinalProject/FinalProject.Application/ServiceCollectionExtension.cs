using FinalProject.Application.Services;
using FinalProject.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FinalProject.Application
{
    public static class ServiceCollectionExtension 
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}

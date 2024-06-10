namespace FinalProject.WebMVC.Controllers
{
    public class DemoController
    {
    }

    class ServiceA : IServiceA
    {
        private Guid id;
        public ServiceA()
        {
            id = Guid.NewGuid();
        }

        public string GetId() => id.ToString();
    }

    interface IServiceA
    {
        string GetId();
    }

    public static class SeriveAExtension
    {
        public static void AddServiceA(this IServiceCollection services)
        {
            services.AddTransient<IServiceA, ServiceA>();
            services.AddScoped<IServiceA, ServiceA>();
            services.AddSingleton<IServiceA, ServiceA>();

        }
    }
}

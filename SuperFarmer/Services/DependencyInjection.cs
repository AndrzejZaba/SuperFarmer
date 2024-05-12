using SuperFarmer.Interfaces;

namespace SuperFarmer.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            services.AddScoped<IGamePreparationService, GamePreparationService>();
            services.AddScoped<IGameDataService, GameDataService>();

            return services;
        }
    }
}

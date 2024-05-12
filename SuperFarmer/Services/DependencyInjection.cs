using SuperFarmer.Interfaces;

namespace SuperFarmer.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            services.AddScoped<IGamePreparationService, GamePreparationService>();
            services.AddScoped<IGameDataService, GameDataService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IDiceService, DiceService>();
            services.AddScoped<IAnimalService, AnimalService>();

            return services;
        }
    }
}

using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.TrainRails;

public static class TrainRailsServiceCollectionExtensions
{
    public static IServiceCollection AddTrainRailsServices(this IServiceCollection services)
    {
        // TODO create weighted selector
        services.AddSingleton<TrainRailsSolver>()
            ;
            // .AddSingleton<IPossibility, PossibilityA>()
            // .AddSingleton<IPossibility, PossibilityB>()
            // .AddSingleton<IPossibility, PossibilityC>()
        return services;
    }
}
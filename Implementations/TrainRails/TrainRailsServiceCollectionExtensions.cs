using WaveFunctionCollapse.Implementations.TrainRails.Possibilities;
using WaveFunctionCollapse.Implementations.TrainRails;
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.Letters;

public static class TrainRailsServiceCollectionExtensions
{
    public static IServiceCollection AddTrainRailsServices(this IServiceCollection services)
    {
        services.AddSingleton<TrainRailsSolver>()
            // .AddSingleton<IPossibility, PossibilityA>()
            // .AddSingleton<IPossibility, PossibilityB>()
            // .AddSingleton<IPossibility, PossibilityC>()
            .AddSingleton<IPossibility, PossibilityEmpty>();
        return services;
    }
}
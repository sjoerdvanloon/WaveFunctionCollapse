using WaveFunctionCollapse.Implementations.Letters.Possibilities;
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.Letters;

public static class LetterServiceCollectionExtensions
{
    public static IServiceCollection AddLettersServices(this IServiceCollection services)
    {
        services.AddSingleton<LetterSolver>()
            .AddSingleton<IPossibility, PossibilityA>()
            .AddSingleton<IPossibility, PossibilityB>()
            .AddSingleton<IPossibility, PossibilityC>()
            .AddSingleton<IPossibility, PossibilityEmpty>();
        return services;
    }
}
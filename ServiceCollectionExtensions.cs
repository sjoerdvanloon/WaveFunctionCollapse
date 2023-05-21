using Microsoft.Extensions.DependencyInjection;
using WaveFunctionCollapse.Algorithm;
using WaveFunctionCollapse.Algorithm.InitialPossibilityGenerator;
using WaveFunctionCollapse.Algorithm.PossibilitySelectors;
using WaveFunctionCollapse.Grids;
using WaveFunctionCollapse.Implementations.Letters;
using WaveFunctionCollapse.Implementations.Letters.Possibilities;
using WaveFunctionCollapse.Possibilities;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWaveFunctionCollapseServices(this IServiceCollection services)
    {
        services
            .AddSingleton<ConsoleGridRenderer>(x =>
            {
                var outputHelper = x.GetRequiredService<ITestOutputHelper>();
                return new ConsoleGridRenderer(outputHelper.WriteLine);
            })
            .AddSingleton<ImageGridRenderer>(x =>
                new ImageGridRenderer($@"C:\temp\{DateTime.Now.ToString("yy-MM-ddhhmm")}.png"))
            .AddSingleton<ICellSelector, RandomizedCellSelector>()
            .AddSingleton<ILowestEntropyCellFinder, LowestEntropyCellFinder>()
            .AddSingleton<Random>(sp => new Random(10))
            .AddSingleton<IPossibilitySelector, RandomizedPossibilitySelector>()
            .AddSingleton<IInitialPossibilityGenerator, InitiallyEverythingIsPossibleGenerator>()
            .AddSingleton<GridFactory>()
            .AddSingleton<NeighbourGenerator>()
            ;

        return services;
    }


    public static IServiceCollection AddLetterServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IPossibility, PossibilityEmpty>()
            .AddSingleton<IPossibility, PossibilityA>()
            .AddSingleton<IPossibility, PossibilityB>()
            .AddSingleton<IPossibility, PossibilityC>()
            .AddSingleton<ISolver, LetterSolver>();
        return services;
    }
}
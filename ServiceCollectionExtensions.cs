using Microsoft.Extensions.DependencyInjection;
using WaveFunctionCollapse.Algorithm;
using WaveFunctionCollapse.Possibilities;
using WaveFunctionCollapse.Possibilities.Letters;
using WaveFunctionCollapse.Renderers;
using WaveFunctionCollapse.Tests;
using Xunit.Abstractions;

namespace WaveFunctionCollapse;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this ServiceCollection services, ITestOutputHelper outputHelper)
    {
        services
            .AddSingleton(outputHelper)
            .AddSingleton<IGridRenderer>(x => new ConsoleGridRenderer(outputHelper.WriteLine))
            .AddSingleton<ICellSelector, RandomizedCellSelector>()
            .AddSingleton<ILowestEntropyCellFinder, LowestEntropyCellFinder>()
            .AddSingleton<GridAutoFiller>()
            .AddSingleton<Random>(sp => new Random(10))
            .AddSingleton<IPossibility, PossibilityEmpty>()
            .AddSingleton<IPossibility, PossibilityA>()
            .AddSingleton<IPossibility, PossibilityB>()
            .AddSingleton<IPossibility, PossibilityC>()
            .AddSingleton<IPossibilitySelector, RandomizedPossibilitySelector>()
            .AddSingleton<CellContextGenerator>()
            ;

        return services;
    }
}
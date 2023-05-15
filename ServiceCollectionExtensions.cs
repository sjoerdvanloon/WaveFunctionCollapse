using Microsoft.Extensions.DependencyInjection;
using WaveFunctionCollapse.Algorithm;
using WaveFunctionCollapse.Algorithm.InitialPossibilityGenerator;
using WaveFunctionCollapse.Algorithm.PossibilitySelectors;
using WaveFunctionCollapse.Implementations.Letters;
using WaveFunctionCollapse.Implementations.Letters.Possibilities;
using WaveFunctionCollapse.Possibilities;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, ITestOutputHelper outputHelper)
    {
        services
            .AddSingleton(outputHelper)
            .AddSingleton<ConsoleGridRenderer>(x => 
                new ConsoleGridRenderer(outputHelper.WriteLine))
            .AddSingleton<ImageGridRenderer>(x => 
                new ImageGridRenderer($@"C:\temp\{DateTime.Now.ToString("yy-MM-ddhhmm")}.png"))
            .AddSingleton<ICellSelector, RandomizedCellSelector>()
            .AddSingleton<ILowestEntropyCellFinder, LowestEntropyCellFinder>()
            .AddSingleton<GridAutoFiller>()
            .AddSingleton<Random>(sp => new Random(10))
            .AddSingleton<IPossibilitySelector, RandomizedPossibilitySelector>()
            .AddSingleton<IInitialPossibilityGenerator, InitialPossibilityGenerator>()
            .AddSingleton<CellContextGenerator>()
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
using Microsoft.Extensions.DependencyInjection;
using WaveFunctionCollapse.Algorithm;
using WaveFunctionCollapse.Grids;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse.Tests;

public  class UnitTestBase<TGridRenderer> where TGridRenderer : IGridRenderer
{
    private readonly ServiceProvider _serviceProvider;
    private readonly TGridRenderer _gridRenderer;
    private readonly GridFactory _gridFactory;
    private readonly Random _random;
    private readonly ITestOutputHelper _outputHelper;


    protected UnitTestBase(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;


        var sc =
            new ServiceCollection()
                .AddSingleton(outputHelper)
                .AddWaveFunctionCollapseServices();
        
        Configure(sc);
        
        _serviceProvider = sc
            .BuildServiceProvider();

        // Get services from service provider
        _gridRenderer = _serviceProvider.GetRequiredService<TGridRenderer>();
        _gridFactory = _serviceProvider.GetRequiredService<GridFactory>();
        _random = _serviceProvider.GetRequiredService<Random>();
    }
    protected void WriteLine(string input) => _outputHelper.WriteLine(input);

    protected virtual IServiceCollection Configure(IServiceCollection sc)
    {
        return sc;
    }

    protected Grid CreateGrid(int height, int width)
    {
        return _gridFactory.CreateGrid(height, width);
    }

    protected TGridRenderer GetGridRenderer()
    {
        return _gridRenderer;
    }

    protected void DrawGrid(Grid grid)
    {
        GetGridRenderer().DrawGrid(grid);
    }

    protected TService GetRequiredService<TService>() where TService : notnull
    {
        return _serviceProvider.GetRequiredService<TService>();
    }
}
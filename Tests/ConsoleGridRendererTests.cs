using Microsoft.Extensions.DependencyInjection;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse.Tests;

public class ConsoleGridRendererTests
{
    private readonly ServiceProvider _serviceProvider;
    private  IGridRenderer _gridRenderer => _serviceProvider.GetRequiredService<IGridRenderer>();

    public ConsoleGridRendererTests(ITestOutputHelper outputHelper)
    {
        
        _serviceProvider = new ServiceCollection().AddServices(outputHelper).BuildServiceProvider();

    }
    
    [Fact]
    public void DrawGrid_ShouldRender()
    {
        var grid = new Grid(4, 4);
        
        _gridRenderer.DrawGrid(grid);
    }
}
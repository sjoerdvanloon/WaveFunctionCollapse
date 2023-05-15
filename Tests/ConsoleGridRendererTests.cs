using Microsoft.Extensions.DependencyInjection;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse.Tests;

public class ConsoleGridRendererTests
{
    private readonly ServiceProvider _serviceProvider;
    private  ConsoleGridRenderer _gridRenderer => _serviceProvider.GetRequiredService<ConsoleGridRenderer>();

    public ConsoleGridRendererTests(ITestOutputHelper outputHelper)
    {
        
        _serviceProvider = new ServiceCollection().AddServices(outputHelper).BuildServiceProvider();

    }
    
    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 4)]
    [InlineData(2, 4)]
    [InlineData(4, 1)]
    [InlineData(2, 2)]
    [InlineData(4, 4)]
    [InlineData(16, 16)]
    public void DrawGrid_ShouldRender(int height, int width)
    {
        var grid = new Grid(height, width);
        
        _gridRenderer.DrawGrid(grid);
    }
}
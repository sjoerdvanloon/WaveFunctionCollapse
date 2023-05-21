using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse.Tests.Renderers;

public class ConsoleGridRendererTests : UnitTestBase<ConsoleGridRenderer>
{
    private ConsoleGridRenderer _gridRenderer => GetGridRenderer();
    public ConsoleGridRendererTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
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
        var grid = CreateGrid(height, width);
        
        _gridRenderer.DrawGrid(grid);
    }
}
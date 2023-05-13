using Microsoft.Extensions.DependencyInjection;
using WaveFunctionCollapse.Algorithm;
using WaveFunctionCollapse.Possibilities;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse.Tests;

public class GridAutoFillerTests
{
    private readonly ServiceProvider _serviceProvider;
    private  GridAutoFiller _sut => _serviceProvider.GetRequiredService<GridAutoFiller>();
    private  IGridRenderer _gridRenderer => _serviceProvider.GetRequiredService<IGridRenderer>();

    public GridAutoFillerTests(ITestOutputHelper outputHelper)
    {
        
        // Create service collection with service provider
        _serviceProvider = new ServiceCollection().AddServices(outputHelper).BuildServiceProvider();
        

    }

 

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(30)]
    public void FillGrid_ShouldWorkForTheNumberOfStepsPassed(int steps)
    {
        var grid = new Grid(4, 4);
        
        _sut.FillGrid(grid, steps);
        
        _gridRenderer.DrawGrid(grid);
    }
    
   
}
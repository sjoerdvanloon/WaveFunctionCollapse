using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WaveFunctionCollapse.Algorithm;
using WaveFunctionCollapse.CellContents;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse.Tests;

public class CellContextGeneratorTests
{
    private readonly ServiceProvider _serviceProvider;
    private  ConsoleGridRenderer _gridRenderer => _serviceProvider.GetRequiredService<ConsoleGridRenderer>();
    private  CellContextGenerator _sut => _serviceProvider.GetRequiredService<CellContextGenerator>();


    public CellContextGeneratorTests(ITestOutputHelper outputHelper)
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
    public void FromGrid_ShouldCountCorrectNumberOfNeighbours(int height, int width)
    {
        // Arrange
        var grid = new Grid(height, width);
        
        // Act
       var contexts =  _sut.FromGrid(grid);
        
        // Assert
        // Set text content for each cell with the number of neighbors and lastPossibility count
        foreach (var context in contexts)
        {
            var content = new TextOnlyCellContent($"{context.GetNeighbourContexts().Length}") ;
            context.Cell.UpdateCellContent(content);
        }
        _gridRenderer.DrawGrid(grid);
    }
}
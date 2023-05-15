using FluentAssertions;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse.Tests;

public class GridTests
{
    private readonly ITestOutputHelper _outputHelper;

    public GridTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    
    [Theory]
    [InlineData(4, 4, 16)]
    [InlineData(1, 4, 4)]
    [InlineData(4, 2, 8)]
    public void GetCells_ShouldReturnCorrectNumber(int height, int width, int expectedCount)
    {
        var grid = new Grid(height, width);

        var cells = grid.GetCells();
        
        cells.Should().HaveCount(expectedCount);
        
        // loop through 1 dimensional cell array and print out the x and y position of each cell
        foreach (var cell in cells)
        {
            _outputHelper.WriteLine($"Cell at x {cell.XPosition}, y {cell.YPosition}");
        }
    }
    
    [Theory]
    [InlineData(0, 0, 0,0)]
    [InlineData(1, 2, 1,2)]
    [InlineData(5, 2, 5,2)]
    public void GetCell_ShouldReturnCorrect(int inputX, int inputY, int outputX, int outputY) 
    {
        var grid = new Grid(6,3);

        var cell = grid.GetCell(inputX, inputY);
        
        // Assert
        var cells = grid.GetCells();
        
        _outputHelper.WriteLine($"Cell: {cell}");
        cell.XPosition.Should().Be(outputX);
        cell.YPosition.Should().Be(outputY);
    }
}
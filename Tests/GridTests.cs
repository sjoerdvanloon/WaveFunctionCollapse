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
    public void GetCells_ShouldReturnCorrectNumber(int height, int width, int expectedCount)
    {
        var grid = new Grid(height, width);

        var cells = grid.GetCells();
        
        cells.Should().HaveCount(expectedCount);
    }
}
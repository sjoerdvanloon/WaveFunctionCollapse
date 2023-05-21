using FluentAssertions;
using WaveFunctionCollapse.CellContents;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse.Tests.Grids;

public class GridTests : UnitTestBase<ConsoleGridRenderer>
{
    public GridTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }


    [Theory]
    [InlineData(4, 4, 16)]
    [InlineData(1, 4, 4)]
    [InlineData(4, 2, 8)]
    public void GetCells_ShouldReturnCorrectNumber(int height, int width, int expectedCount)
    {
        var grid = CreateGrid(height, width);

        var cells = grid.GetCells();

        cells.Should().HaveCount(expectedCount);

        // loop through 1 dimensional cell array and print out the x and y position of each cell
        foreach (var cell in cells)
        {
            WriteLine($"Cell at x {cell.XPosition}, y {cell.YPosition}");
        }
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(1, 2, 1, 2)]
    [InlineData(5, 2, 5, 2)]
    public void GetCell_ShouldReturnCorrectPositions(int inputX, int inputY, int outputX, int outputY)
    {
        var grid = CreateGrid(3, 6);

        var cell = grid.GetCellBasedOnCoordinates(inputX, inputY);

        // Assert
        var cells = grid.GetCells();

        WriteLine($"Cell: {cell}");
        cell.XPosition.Should().Be(outputX);
        cell.YPosition.Should().Be(outputY);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 4)]
    [InlineData(2, 4)]
    [InlineData(4, 1)]
    [InlineData(2, 2)]
    [InlineData(4, 4)]
    [InlineData(16, 16)]
    public void Constructor_ShouldGenerateTheCorrectNeighbours(int height, int width)
    {
        // Arrange
        var grid = CreateGrid(height, width);

        // Act

        // Assert

        grid.IterateThroughCells((c) =>
        {
            var content = new TextOnlyCellContent($"{c.Neighbours.GetDirectNeighbours()}");
            c.SetCellContent(content);
        });

        DrawGrid(grid);
    }
}
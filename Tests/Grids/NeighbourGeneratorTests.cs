using FluentAssertions;
using WaveFunctionCollapse.Grids;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse.Tests.Grids;

public class NeighbourGeneratorTests : UnitTestBase<ConsoleGridRenderer>
{
    private readonly NeighbourGenerator _sut;


    public NeighbourGeneratorTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _sut =GetRequiredService<NeighbourGenerator>();
    }

    [Theory]
    [InlineData(1, 1, 0, 0, 0)]
    [InlineData(1, 4, 0, 0, 1)]
    [InlineData(2, 4, 0, 0, 3)]
    [InlineData(4, 4, 0, 0, 3)]
    [InlineData(1, 4, 1, 0, 2)]
    [InlineData(2, 4, 2, 0, 5)]
    [InlineData(4, 4, 2, 0, 5)]
    [InlineData(6, 6, 2, 2, 8)]
    public void ForCell_ShouldGenerateTheCorrectNeighbours(int height, int width, int xPos, int yPos, int expectedCount)
    {
        // Arrange
        var grid = CreateGrid(height, width);
        var cell = grid.GetCellBasedOnCoordinates(xPos, yPos);
        // Act
        var neighboursForCell = _sut.ForCell(cell, grid);

        // Assert
        DrawGrid(grid);

        neighboursForCell.Should().NotBeNull();
        var directNeighbours = neighboursForCell.GetDirectNeighbours();
        directNeighbours.Should().HaveCount(expectedCount);
    }

   
}
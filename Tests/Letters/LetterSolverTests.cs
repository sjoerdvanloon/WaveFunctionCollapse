using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WaveFunctionCollapse.Algorithm;
using WaveFunctionCollapse.Grids;
using WaveFunctionCollapse.Implementations.Letters;
using WaveFunctionCollapse.Implementations.Letters.Possibilities;
using WaveFunctionCollapse.Possibilities;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse.Tests.Letters;

public class LetterSolverTests : UnitTestBase<ConsoleGridRenderer>
{
    private LetterSolver _sut => GetRequiredService<LetterSolver>();

    public LetterSolverTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    protected override IServiceCollection Configure(IServiceCollection sc)
    {
        sc
            .AddSingleton<LetterSolver>()
            .AddSingleton<IPossibility, PossibilityA>()
            .AddSingleton<IPossibility, PossibilityB>()
            .AddSingleton<IPossibility, PossibilityC>()
            .AddSingleton<IPossibility, PossibilityEmpty>()
            ;
        return sc;
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(400)]
    [InlineData(4000)]
    public void Solve_ShouldWorkForTheNumberOfStepsPassed(int steps)
    {
        // Arrange
        var grid = CreateGrid(8, 8);

        // Act
        _sut.Solve(grid, steps);

        // Assert
        DrawGrid(grid);
        
        if (steps > 0)
        {
           // AssertCellLetters(grid, 3, 3, WaveFunctionCollapse.Implementations.Letters.Letters.Empty);
        }

        if (steps > 1)
        {
        }

    }

    private void AssertCellLetters(
        Grid grid,
        int xPosition,
        int yPosition,
        params WaveFunctionCollapse.Implementations.Letters.Letters[] letters)
    {
        if (letters == null)
            throw new ArgumentNullException(nameof(letters));
        if (!letters.Any())
            throw new ArgumentException("Value cannot be an empty collection.", nameof(letters));
        
        var cell = grid.GetCellBasedOnCoordinates(xPosition, yPosition);
        cell.CellContent.Should().BeOfType<LetterCellContent>();
        var letterCellContent = (LetterCellContent)cell.CellContent;
        
        letterCellContent.Letters.Should()
            .HaveCount(letters.Length).And
            .Contain(letters);
    }
}
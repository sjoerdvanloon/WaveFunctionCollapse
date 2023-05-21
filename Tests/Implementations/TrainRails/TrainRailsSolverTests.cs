using WaveFunctionCollapse.Implementations.TrainRails;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse.Tests.Implementations.TrainRails;

public class TrainRailsSolverTests : UnitTestBase<ConsoleGridRenderer>
{
    private readonly TrainRailsSolver _sut;


    public TrainRailsSolverTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _sut = GetRequiredService<TrainRailsSolver>();
    }

    protected override IServiceCollection Configure(IServiceCollection sc)
    {
        sc.AddTrainRailsServices();
        return sc;
    }

    [Theory]
    [InlineData(1, 8,8)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(10)]
    [InlineData(4000)]
    public void Solve_ShouldWorkForTheNumberOfStepsPassed(int steps , int height = 4, int width = 4)
    {
        // Arrange
        var grid = CreateGrid(height, width);

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
}
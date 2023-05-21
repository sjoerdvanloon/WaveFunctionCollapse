using FluentAssertions;
using WaveFunctionCollapse.Implementations.TrainRails;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse.Tests.Renderers;

public class ImageGridRendererTests : UnitTestBase<ImageGridRenderer>
{
    private readonly TrainRailsSolver _solver;
    private  ImageGridRenderer _gridRenderer => GetGridRenderer();

    public ImageGridRendererTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _solver = GetRequiredService<TrainRailsSolver>();

    }

    protected override IServiceCollection Configure(IServiceCollection sc)
    {
        return sc.AddTrainRailsServices();
    }

    [Fact]
    public void DrawGrid_ShouldRender()
    {
        // Sanity check 
        var rootType = typeof(ImageGridRenderer);
        var resourceNames = rootType.Assembly.GetManifestResourceNames();
        resourceNames.Should().HaveCount(8);
        
        // Arrange
        var grid = CreateGrid(8, 8);
        _solver.Solve(grid, 4000);
        
        // Act
        _gridRenderer.DrawGrid(grid);

        // Assert
        WriteLine(_gridRenderer.Path);
    }
}
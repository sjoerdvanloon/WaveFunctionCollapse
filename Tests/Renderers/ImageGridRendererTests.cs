using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WaveFunctionCollapse.Algorithm;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse.Tests.Renderers;

public class ImageGridRendererTests : UnitTestBase<ImageGridRenderer>
{
    private  ImageGridRenderer _gridRenderer => GetGridRenderer();

    public ImageGridRendererTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    [Fact]
    public void DrawGrid_ShouldRender()
    {
        var rootType = typeof(ImageGridRenderer);
        var resourceNames = rootType.Assembly.GetManifestResourceNames();
        resourceNames.Should().HaveCount(5);

        var grid = CreateGrid(4, 4);
        //_autoFiller.FillGrid(grid, 400);

        _gridRenderer.DrawGrid(grid);

        WriteLine(_gridRenderer.Path);
    }
}
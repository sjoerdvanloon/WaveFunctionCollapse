using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WaveFunctionCollapse.Algorithm;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse.Tests;

public class ImageGridRendererTests
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly ServiceProvider _serviceProvider;
    private  ImageGridRenderer _gridRenderer => _serviceProvider.GetRequiredService<ImageGridRenderer>();
 private  GridAutoFiller _autoFiller => _serviceProvider.GetRequiredService<GridAutoFiller>();

    public ImageGridRendererTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;

        _serviceProvider = new ServiceCollection().AddServices(outputHelper).BuildServiceProvider();
    }
    
    [Fact]
    public void DrawGrid_ShouldRender()
    {
        var rootType = typeof(ImageGridRenderer); 
        var resourceNames = rootType.Assembly.GetManifestResourceNames();
        resourceNames.Should().HaveCount(5);
        
        var grid = new Grid(4, 4);
        _autoFiller.FillGrid(grid, 400);
        
        _gridRenderer.DrawGrid(grid);
        
        _outputHelper.WriteLine(_gridRenderer.Path);
    }
}
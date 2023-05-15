using Microsoft.Extensions.DependencyInjection;
using WaveFunctionCollapse.Algorithm;
using WaveFunctionCollapse.Implementations.Letters;
using WaveFunctionCollapse.Renderers;
using Xunit.Abstractions;

namespace WaveFunctionCollapse.Tests.Letters;

public class LetterSolverTests
{
    private readonly ServiceProvider _serviceProvider;
    private  LetterSolver _sut => (LetterSolver)_serviceProvider.GetRequiredService<ISolver>();
    private  ConsoleGridRenderer _gridRenderer => _serviceProvider.GetRequiredService<ConsoleGridRenderer>();

    public LetterSolverTests(ITestOutputHelper outputHelper)
    {
        
        // Create service collection with service provider
        _serviceProvider = new ServiceCollection()
            .AddServices(outputHelper)
            .AddLetterServices()
            .BuildServiceProvider();
        

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
    public void FillGrid_ShouldWorkForTheNumberOfStepsPassed(int steps)
    {
        var grid = new Grid(4, 4);
        
        _sut.Solve(grid, steps);
        
        _gridRenderer.DrawGrid(grid);
    }
    
   
}
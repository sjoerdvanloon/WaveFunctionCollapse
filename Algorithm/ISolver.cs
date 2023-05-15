namespace WaveFunctionCollapse.Algorithm;

public interface ISolver
{
    public bool Solve(Grid grid, int maxIterations = 1000);
}
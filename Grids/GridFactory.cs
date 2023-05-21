namespace WaveFunctionCollapse.Grids;

public class GridFactory
{
    private readonly NeighbourGenerator _neighbourGenerator;

    public GridFactory(NeighbourGenerator neighbourGenerator)
    {
        _neighbourGenerator = neighbourGenerator ?? throw new ArgumentNullException(nameof(neighbourGenerator));
    }
    public Grid CreateGrid(int height, int width)
    {
        return new Grid(height, width, _neighbourGenerator);
    }
}
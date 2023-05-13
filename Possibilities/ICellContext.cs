namespace WaveFunctionCollapse.Possibilities;

public interface ICellContext
{
    public Cell Cell { get; }
    public CellContext[] GetNeighbourContexts();
    public IPossibility[]? LastPossibilities { get; }
}

public class CellContext : ICellContext
{
    public Cell Cell { get; }
    private CellContext[]? _neighbours;
    public IPossibility[]? LastPossibilities { get; }

    public CellContext(Cell cell)
    {
        Cell = cell;
    }

    public void SetNeighbours(IEnumerable<CellContext> cellContexts)
    {
        _neighbours = cellContexts.ToArray();
    }

    public CellContext[] GetNeighbourContexts()
    {
        return _neighbours ?? Array.Empty<CellContext>();
    }
}
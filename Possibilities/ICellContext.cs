namespace WaveFunctionCollapse.Possibilities;

public interface ICellContext
{
    public Cell Cell { get; }
    public CellContext[] GetNeighbourContexts();
    public IPossibility[]? LastPossibilities { get; }
    public void PickPossibility(IPossibility possibility);
    public bool HasPickedPossibility();
}

public class CellContext : ICellContext
{
    public Cell Cell { get; }
    private CellContext[]? _neighbours;
    public IPossibility[]? LastPossibilities { get; set; }
    private IPossibility? _pickedPossibility { get; set; }

    public CellContext(Cell cell)
    {
        Cell = cell;
    }

    public void SetNeighbours(IEnumerable<CellContext> cellContexts)
    {
        if (_neighbours is not null)
        {
            throw new InvalidOperationException("Neighbours are already set");
        }
        _neighbours = cellContexts.ToArray();
    }
    
    public void PickPossibility(IPossibility possibility)
    {
        if (_pickedPossibility is not null)
        {
            throw new InvalidOperationException("Possibility is already picked");
        }
        LastPossibilities = new[] {possibility};
        _pickedPossibility = possibility;
    }

    public bool HasPickedPossibility()
    {
        return _pickedPossibility is not null;
    }

    public CellContext[] GetNeighbourContexts()
    {
        return _neighbours ?? Array.Empty<CellContext>();
    }
}
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
    public IPossibility? PickedPossibility { get; set; }

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
        if (PickedPossibility is not null)
        {
            throw new InvalidOperationException("Possibility is already picked");
        }
        LastPossibilities = new[] {possibility};
        PickedPossibility = possibility;
    }

    public bool HasPickedPossibility()
    {
        return PickedPossibility is not null;
    }

    public CellContext[] GetNeighbourContexts()
    {
        return _neighbours ?? Array.Empty<CellContext>();
    }
    
    public override string ToString()
    {
        return $"{Cell} with {(PickedPossibility is not null ? "picked possibility" :  LastPossibilities?.Length.ToString())}";
    }
}
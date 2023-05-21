using WaveFunctionCollapse.Grids;

namespace WaveFunctionCollapse.Possibilities;

public interface ICellContext
{
    public Cell Cell { get; }
    public IPossibility[]? LastPossibilities { get; }
    public void PickPossibility(IPossibility possibility);
    public bool HasPickedPossibility();
}

public class CellContext : ICellContext
{
    public Cell Cell { get; }
    public IPossibility[]? LastPossibilities { get; set; }
    public IPossibility? PickedPossibility { get; set; }

    public CellContext(Cell cell)
    {
        Cell = cell;
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
    
    public override string ToString()
    {
        return $"{Cell} with {(PickedPossibility is not null ? "picked possibility" :  LastPossibilities?.Length.ToString())}";
    }
}
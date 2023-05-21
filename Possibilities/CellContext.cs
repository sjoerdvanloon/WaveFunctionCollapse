using WaveFunctionCollapse.Grids;

namespace WaveFunctionCollapse.Possibilities;

public class CellContext : ICellContext
{
    public Cell Cell { get; }
    public IPossibility[] LastPossibilities { get; set; }
    public IPossibility? PickedPossibility { get; set; }
    public bool HasPickedPossibility => PickedPossibility is not null;

    public CellContext(Cell cell, IEnumerable<IPossibility> initialPossibilities)
    {
        Cell = cell;
        LastPossibilities = initialPossibilities.ToArray();
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
    
    public override string ToString()
    {
        return $"{Cell} with {(PickedPossibility is not null ? "picked possibility" :  LastPossibilities?.Length.ToString())}";
    }
}
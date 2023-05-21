using WaveFunctionCollapse.Grids;

namespace WaveFunctionCollapse.Possibilities;

public interface ICellContext
{
    public Cell Cell { get; }
    public IPossibility[]? LastPossibilities { get; }
    public void PickPossibility(IPossibility possibility);
    public bool HasPickedPossibility { get; }
}
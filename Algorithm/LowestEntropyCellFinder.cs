namespace WaveFunctionCollapse.Algorithm;

public interface ILowestEntropyCellFinder
{
    Cell[] FindLowestEntropyCells(EntropisedCell[] cells);
}

public class LowestEntropyCellFinder : ILowestEntropyCellFinder
{
    public LowestEntropyCellFinder()
    {
      
    }
   
    public Cell[] FindLowestEntropyCells(EntropisedCell[] cells)
    {
        int lowestEntropy = cells.Min(x => x.Entropy);
        return cells.Where(x => x.Entropy == lowestEntropy).Select(x => x.Cell).ToArray();
    }
}
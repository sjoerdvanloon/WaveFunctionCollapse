namespace WaveFunctionCollapse.Grids;

public class Neighbour
{
    public Cell FromCell { get; }
    public Cell Cell { get; }
    public NeighbourDirection Direction { get; }
    public Neighbour(Cell fromCell, NeighbourDirection neighbourDirection, Cell cell)
    {
        FromCell = fromCell;
        Cell = cell;
        Direction = neighbourDirection;
    }
    
}
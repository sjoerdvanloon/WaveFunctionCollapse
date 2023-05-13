namespace WaveFunctionCollapse.Algorithm;

public interface ICellSelector
{
    Cell SelectCell(Cell[] cells);
}

public class RandomizedCellSelector : ICellSelector
{
    private readonly Random _random;

    public RandomizedCellSelector(Random random)
    {
        _random = random;
    }

    public Cell SelectCell(Cell[] cells)
    {
        var index = _random.Next(0, cells.Length);
        return cells[index];
    }
}
namespace WaveFunctionCollapse;

public class Grid
{
    private int _width;
    private int _height;
    private readonly Cell[] _cells;

    public int Height => _height;
    public int Width => _width;

    public Grid(int height, int width)
    {
        if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
        if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));

        _height = height;
        _width = width;

        _cells = new Cell[height * width];

        // Generate all cells
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                var oneDPosition = TryGetOneDPosition(x, y);
                _cells[oneDPosition] = Cell.Create(x, y);
            }
        }
    }

    public Cell[] GetCells()
    {
        return _cells.ToArray();
    }

    private int GetOneDPosition(int xPosition, int yPosition)
    {
        return (xPosition * _width) + yPosition;
    }

    public Cell GetCell(int xPosition, int yPosition)
    {
        var oneDPosition = TryGetOneDPosition(xPosition, yPosition);
        return _cells[oneDPosition];
    }

    private int TryGetOneDPosition(int xPosition, int yPosition)
    {
        var oneDPosition = GetOneDPosition(xPosition, yPosition);
        if (_cells.Length <= oneDPosition)
            throw new ArgumentOutOfRangeException(
                $"1d position {oneDPosition} not in bound of 1d array of {_cells.Length}.",
                nameof(oneDPosition));

        return oneDPosition;
    }
}
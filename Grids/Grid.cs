namespace WaveFunctionCollapse.Grids;

public class Grid
{
    private readonly NeighbourGenerator _neighbourGenerator;

    private readonly int _width;
    private readonly int _height;
    private readonly Cell[] _cellsInOneDimension;

    public int Height => _height;
    public int Width => _width;

    public Grid(int height, int width, NeighbourGenerator neighbourGenerator)
    {
        if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
        if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));

        _height = height;
        _width = width;
        _neighbourGenerator = neighbourGenerator;

        _cellsInOneDimension = new Cell[height * width];

        // Generate all cells
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var oneDPosition = GetOneDimensionalPosition(x, y);
                _cellsInOneDimension[oneDPosition] = Cell.CreateBasedOnCoordinates(x, y);
            }
        }

        // Generate neighbours
        IterateThroughCells((cell) =>
        {
            var neighbours = _neighbourGenerator.ForCell(cell, this);
            cell.SetNeighbours(neighbours);
        });
    }

    public void IterateThroughCells(Action<Cell> actionPerCell)
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                var oneDPosition = GetOneDimensionalPosition(x, y);
                var cell = _cellsInOneDimension[oneDPosition];
                actionPerCell(cell);
            }
        }
    }

    public Cell[] GetCells()
    {
        return _cellsInOneDimension.ToArray();
    }

    private int GetOneDPosition(int xPosition, int yPosition)
    {
        return (xPosition) + yPosition * _width;
    }

    // public Neighbours GetNeighboursBasedOnCell(Cell cell)
    // {
    //     return null;
    // }

    public Cell GetCellBasedOnCoordinates(int xPosition, int yPosition)
    {
        var oneDPosition = GetOneDimensionalPosition(xPosition, yPosition);
        return _cellsInOneDimension[oneDPosition];
    }

    private int GetOneDimensionalPosition(int xPosition, int yPosition)
    {
        var oneDPosition = GetOneDPosition(xPosition, yPosition);
        if (_cellsInOneDimension.Length <= oneDPosition)
            throw new ArgumentOutOfRangeException(
                $"1d position {oneDPosition} not in bound of 1d array of {_cellsInOneDimension.Length}.",
                nameof(oneDPosition));

        return oneDPosition;
    }
}
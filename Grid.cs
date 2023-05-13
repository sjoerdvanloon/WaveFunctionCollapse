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
        _cells = new Cell[height* width];
        
        // Generate all cells
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            { 
                _cells[y * width + x] = new Cell(x, y);
            }
        }
    }
    public Cell[] GetCells()
    {
        return _cells.ToArray();
    }

    public Cell GetCell(int xPosition, int yPosition)
    {
        return _cells[yPosition * _width + xPosition];
    }
}
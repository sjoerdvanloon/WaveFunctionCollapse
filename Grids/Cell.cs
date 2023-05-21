using System.Diagnostics;
using WaveFunctionCollapse.CellContents;

namespace WaveFunctionCollapse.Grids;

public class Cell
{
    private readonly int _xPosition;
    private readonly int _yPosition;

    private ICellContent _lastCellContent;
    private Neighbours? _neighbours;

    public int XPosition => _xPosition;
    public int YPosition => _yPosition;
    public int Row => _yPosition +1;
    public int Column => _xPosition +1;
    
    public ICellContent CellContent => _lastCellContent ?? throw new InvalidOperationException("CellContent is null");
    public Neighbours Neighbours
    {
        get
        {
            Debug.Assert(_neighbours != null, nameof(_neighbours) + " != null");
            return _neighbours;
        }
    }

    private Cell(int xPosition, int yPosition)
    {
        _xPosition = xPosition;
        _yPosition = yPosition;

        _lastCellContent = new PositionCellContent(this);

        _neighbours = null;

    }
    
    public static Cell CreateBasedOnCoordinates(int xPosition, int yPosition)
    {
        if (xPosition < 0)
            throw new ArgumentOutOfRangeException($"Should be equal or greater than 0", nameof(xPosition));
        
        if (yPosition < 0)
            throw new ArgumentOutOfRangeException($"Should be equal or greater than 0", nameof(yPosition));
        
        return new Cell(xPosition, yPosition);
    }
    
    public static Cell CreateBasedOnGrid(int row, int column)
    {
        if (row <= 0)
            throw new ArgumentOutOfRangeException($"Should be greater than 0", nameof(row));
        
        if (column <= 0)
            throw new ArgumentOutOfRangeException($"Should be greater than 0", nameof(column));
        
        return new Cell(column -1, row-1);
    }

    public void SetNeighbours(Neighbours neighbours)
    {
        if (_neighbours is not null)
            throw new InvalidOperationException("Neighbours already set");
        
        _neighbours = neighbours ?? throw new ArgumentNullException(nameof(neighbours));
    }
    
    public void SetCellContent(ICellContent cellContent)
    {
        _lastCellContent = cellContent ?? throw new ArgumentNullException(nameof(cellContent));
    }
    
    public override string ToString()
    {
        return $"Cell at ({_xPosition}, {_yPosition}) with content {_lastCellContent.GetFriendlyText()}";
    }

}
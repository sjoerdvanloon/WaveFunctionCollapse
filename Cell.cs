using WaveFunctionCollapse.CellContents;

namespace WaveFunctionCollapse;

public class Cell
{
    private readonly int _xPosition;
    private readonly int _yPosition;

    private ICellContent _lastCellContent ;

    public int XPosition => _xPosition;
    public int YPosition => _yPosition;
    
    public ICellContent CellContent => _lastCellContent ?? throw new InvalidOperationException("CellContent is null");

    public Cell(int xPosition, int yPosition)
    {
        _xPosition = xPosition;
        _yPosition = yPosition;

        UpdateCellContent(new PositionCellContent(this));
    }
    
    public static Cell Create(int xPosition, int yPosition)
    {
        return new Cell(xPosition, yPosition);
    }
    
    public void UpdateCellContent(ICellContent cellContent)
    {
        if (cellContent is null) throw new ArgumentNullException(nameof(cellContent));

        _lastCellContent = cellContent;
     
    }
    
    public override string ToString()
    {
        return $"Cell at ({_xPosition}, {_yPosition}) with content {_lastCellContent}";
    }

}
using WaveFunctionCollapse.Grids;

namespace WaveFunctionCollapse.CellContents;

public class PositionCellContent : ICellContent
{   
    private readonly int _xPosition;
    private readonly int _yPosition;
    
    public PositionCellContent(Cell cell)
    {
        this._xPosition = cell.XPosition;
        this._yPosition = cell.YPosition;
    }
    public string GetFriendlyText() => $"({_xPosition},{_yPosition})";
}
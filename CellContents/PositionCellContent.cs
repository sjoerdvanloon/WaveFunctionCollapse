using WaveFunctionCollapse.Grids;

namespace WaveFunctionCollapse.CellContents;

public class PositionCellContent : ICellContent
{   
    private readonly int _xPosition;
    private readonly int _yPosition;
    
    public PositionCellContent(Cell cell)
    {
        _xPosition = cell.XPosition;
        _yPosition = cell.YPosition;
    }
    public string GetFriendlyText() => $"({_xPosition},{_yPosition})";
}
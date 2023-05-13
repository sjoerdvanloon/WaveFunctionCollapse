namespace WaveFunctionCollapse.CellContents;

public class EntropyCellContent : ICellContent
{
    private readonly int _entropy;
    private readonly int _xPosition;
    private readonly int _yPosition;
    
    public EntropyCellContent(Cell cell, int entropy)
    {
        this._xPosition = cell.XPosition;
        this._yPosition = cell.YPosition;
        _entropy = entropy;
    }
    public string Text  => $"({_xPosition},{_yPosition},E:{_entropy})";
}
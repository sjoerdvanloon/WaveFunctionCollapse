using WaveFunctionCollapse.CellContents;

namespace WaveFunctionCollapse;

public class Cell
{
    private readonly int _xPosition;
    private readonly int _yPosition;

    private ICellContent? _lastCellContent = null;

    public int XPosition => _xPosition;
    public int YPosition => _yPosition;
    public string Text { get; private set; } = string.Empty;

    public Cell(int xPosition, int yPosition)
    {
        _xPosition = xPosition;
        _yPosition = yPosition;

        UpdateCellContent(new PositionCellContent(this));
    }

    private void UpdateText()
    {
        if (_lastCellContent is null)
        {
            Text = $"({_xPosition},{_yPosition})";
        }
        else
        {
            Text = _lastCellContent.Text;
        }
    }

    public void UpdateCellContent(ICellContent cellContent)
    {
        if (cellContent is null) throw new ArgumentNullException(nameof(cellContent));

        _lastCellContent = cellContent;

        UpdateText();
    }
}
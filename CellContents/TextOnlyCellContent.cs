namespace WaveFunctionCollapse.CellContents;

public class TextOnlyCellContent : ICellContent
{
    private readonly string _text;
    public TextOnlyCellContent( string text)
    {
        _text = text;
    }

    public string Text => _text;
}
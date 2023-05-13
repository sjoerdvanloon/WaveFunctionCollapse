namespace WaveFunctionCollapse.CellContents;

public enum Letters
{
    Empty, A, B, C
}

public class LetterCellContent : ICellContent
{
    public readonly Letters Letter;
    public LetterCellContent(Letters letter)
    {
        Letter = letter;
    }

    public string Text
    {
        get => Letter.ToString();
    }
}
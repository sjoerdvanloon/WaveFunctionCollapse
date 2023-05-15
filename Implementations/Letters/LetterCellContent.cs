using WaveFunctionCollapse.CellContents;

namespace WaveFunctionCollapse.Implementations.Letters;

public enum Letters
{
    Empty, A, B, C
}

public class LetterCellContent : ICellContent
{
    public  Letters[]? Letters { get; }
    public bool Picked { get; set; } = false;
    public bool Error { get; set; } = false;
    
    private LetterCellContent(bool error, bool picked, IEnumerable<Letters>? letters)
    {
        Error = error;
        Picked = picked;
        Letters = letters?.ToArray();
    }
    
    public static LetterCellContent CreateUndetermined(IEnumerable<Letters> letters)
    {
        if (letters == null) throw new ArgumentNullException(nameof(letters));

        var lettersEnumerable = letters as Letters[] ?? letters.ToArray();
        if (!lettersEnumerable.Any())
            throw new ArgumentException("Letters array cannot be empty");
        
        return new LetterCellContent(false, false, letters.ToArray());
    }
    
    public static LetterCellContent CreatePicked(Letters letter)
    {
        return new LetterCellContent(false, true, new Letters[] {letter});
    }
    
    public static LetterCellContent CreateError(string error)
    {
        return new LetterCellContent(true, false, null);
    }

    public string GetFriendlyText()
    {
        if (Error)
            return "Error";

        if (Letters is null)
            return "No letters";

        if (Letters.Length == 1 && Picked)
            return $"{Letters.Single()}";

        if (Letters.Length == 4)
            return "All";
        
        return string.Join(",", Letters);
    }
}
using WaveFunctionCollapse.CellContents;

namespace WaveFunctionCollapse.Implementations.Sudoku;

public class SudokuCellContent : ICellContent
{
    public  int[]? Numbers { get; }
    public bool Picked { get; set; } = false;
    public bool Error { get; set; } = false;
    
    private SudokuCellContent(bool error, bool picked, IEnumerable<int>? numbers)
    {
        Error = error;
        Picked = picked;
        Numbers = numbers?.ToArray();
    }
   
    public static SudokuCellContent CreateUndetermined(IEnumerable<int> numbers)
    {
        if (numbers == null) throw new ArgumentNullException(nameof(numbers));

        var railTypesArray = numbers as int[] ?? numbers.ToArray();
        if (!railTypesArray.Any())
            throw new ArgumentException("Letters array cannot be empty");
        
        return new SudokuCellContent(false, false, numbers.ToArray());
    }
    
    public static SudokuCellContent CreatePicked(int number)
    {
        return new SudokuCellContent(false, true, new int[] {number});
    }
    
    public static SudokuCellContent CreateError(string error)
    {
        return new SudokuCellContent(true, false, null);
    }

    public string GetFriendlyText()
    {
        if (Error)
            return "Error";

        if (Numbers is null)
            return "No numbers";

        if (Numbers.Length == 1 && Picked)
            return $"{Numbers.Single()}";

        if (Numbers.Length == 9)
            return $"All";
        if (Numbers.Length > 4)
            return $"A lot";
        
        return string.Join(",", Numbers);
    }

    
}
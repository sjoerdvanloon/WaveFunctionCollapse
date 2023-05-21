using WaveFunctionCollapse.CellContents;

namespace WaveFunctionCollapse.Implementations.TrainRails;

public class RailCellContent : ICellContent, IEmbeddedResourceContent
{
    public  RailTypes[]? RailTypes { get; }
    public bool Picked { get; set; } = false;
    public bool Error { get; set; } = false;
    
    private RailCellContent(bool error, bool picked, IEnumerable<RailTypes>? letters)
    {
        Error = error;
        Picked = picked;
        RailTypes = letters?.ToArray();
    }
    
    public static RailCellContent CreateUndetermined(IEnumerable<RailTypes> railTypes)
    {
        if (railTypes == null) throw new ArgumentNullException(nameof(railTypes));

        var railTypesArray = railTypes as RailTypes[] ?? railTypes.ToArray();
        if (!railTypesArray.Any())
            throw new ArgumentException("Letters array cannot be empty");
        
        return new RailCellContent(false, false, railTypes.ToArray());
    }
    
    public static RailCellContent CreatePicked(RailTypes railType)
    {
        return new RailCellContent(false, true, new RailTypes[] {railType});
    }
    
    public static RailCellContent CreateError(string error)
    {
        return new RailCellContent(true, false, null);
    }

    public string GetFriendlyText()
    {
        if (Error)
            return "Error";

        if (RailTypes is null)
            return "No rail types";

        if (RailTypes.Length == 1 && Picked)
            return $"{RailTypes.Single().ToAsciiArtString()}";

        if (RailTypes.Length == Enum.GetValues<RailTypes>().Length)
            return $"All";
        if (RailTypes.Length > 4)
            return $"A lot";
        
        return string.Join(",", RailTypes.Select(x=>x.ToAsciiArtString()));
    }

    public string GetEmbeddedResourceName()
    {
        if (RailTypes is null)
            return "blank";
        if (!Picked)
            return "blank";
        if (Error)
            return "blank";

        return RailTypes.Single().ToEmbeddedResourceName();
    }
}
namespace WaveFunctionCollapse.Possibilities.Letters;

public class PossibilityB  : PossibilityBase, IPossibility
{
    public string Name { get; } = "B";
    public string Description { get; } = "B can be followed by A, B or C";

    public PossibilityB()
    {
    }
   
    public string GetText()
    {
        return Name;
    }

  
    
    protected override string[] GetExceptedNames()
    {
        return new []{"A", Name, "C"};
    }
    
    public override CellContents.Letters Letter => CellContents.Letters.B;
}
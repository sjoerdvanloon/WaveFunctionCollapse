namespace WaveFunctionCollapse.Possibilities.Letters;

public class PossibilityEmpty  : PossibilityBase, IPossibility
{
    public string Name { get; } = "Empty";
    public string Description { get; } = "empty can be followed by empty or A";

    public PossibilityEmpty()
    {
    }
   
    public string GetText()
    {
        return Name;
    }

    protected override string[] GetExceptedNames()
    {
        return new []{"A", Name};
    }

    public override CellContents.Letters Letter => CellContents.Letters.Empty;
}
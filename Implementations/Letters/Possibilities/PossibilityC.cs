namespace WaveFunctionCollapse.Possibilities.Letters;

public class PossibilityC : PossibilityBase, IPossibility
{
    public string Name { get; } = "C";
    public string Description { get; } = "C can be followed by B or C";

    public PossibilityC()
    {
    }
   
    public string GetText()
    {
        return Name;
    }

    protected override string[] GetExceptedNames()
    {
        return new []{"B", Name};
    }

    public override CellContents.Letters Letter => CellContents.Letters.C;
}
namespace WaveFunctionCollapse.Possibilities;

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
}
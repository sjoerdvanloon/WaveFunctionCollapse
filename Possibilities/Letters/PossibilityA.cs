namespace WaveFunctionCollapse.Possibilities.Letters;

public class PossibilityA  : PossibilityBase, IPossibility
{
    public string Name { get; } = "A";
    public string Description { get; } = "A can be followed by A, B or empty";

    public PossibilityA()
    {
    }
   
    public string GetText()
    {
        return Name;
    }

    protected override string[] GetExceptedNames()
    {
        return new []{"A", "B", "Empty"};
    }
}
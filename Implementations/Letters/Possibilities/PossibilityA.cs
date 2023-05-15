using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.Letters.Possibilities;

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

    public override Implementations.Letters.Letters Letter => Implementations.Letters.Letters.A;
}
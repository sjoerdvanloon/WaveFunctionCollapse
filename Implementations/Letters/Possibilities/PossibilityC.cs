using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.Letters.Possibilities;

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

    public override Implementations.Letters.Letters Letter => Implementations.Letters.Letters.C;
}
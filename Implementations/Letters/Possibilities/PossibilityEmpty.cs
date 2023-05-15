using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.Letters.Possibilities;

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

    public override Implementations.Letters.Letters Letter => Implementations.Letters.Letters.Empty;
}
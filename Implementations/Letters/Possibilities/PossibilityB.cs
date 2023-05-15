using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.Letters.Possibilities;

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
    
    public override Implementations.Letters.Letters Letter => Implementations.Letters.Letters.B;
}
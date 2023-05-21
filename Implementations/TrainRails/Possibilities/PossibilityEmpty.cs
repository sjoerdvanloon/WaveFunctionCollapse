using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.TrainRails.Possibilities;

public class PossibilityEmpty  : PossibilityBase, IPossibility
{
    public string Name { get; } = "Empty";
    public override RailTypes RailType => RailTypes.Empty;
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

}
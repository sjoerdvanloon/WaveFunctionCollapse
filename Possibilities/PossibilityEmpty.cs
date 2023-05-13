namespace WaveFunctionCollapse.Possibilities;

public class PossibilityEmpty :IPossibility
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

    public bool IsPossible(ICellContext cellContext)
    {
        var neighbourContexts = cellContext.GetNeighbourContexts();
        
        var allPossibilities =
            neighbourContexts
                .SelectMany(x=> x.LastPossibilities ?? Array.Empty<IPossibility>()).ToArray();
        
        if(!allPossibilities.Any())
        {
            return true; // No possibilities, so anything is possible
        }
        
        return allPossibilities.All(x => x.Name == "A" || x.Name == "Empty" );
    }
}
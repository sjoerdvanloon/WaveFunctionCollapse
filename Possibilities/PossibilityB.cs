namespace WaveFunctionCollapse.Possibilities;

public class PossibilityB :IPossibility
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
        
        return allPossibilities.All(x => x.Name == "A" || x.Name == "B" || x.Name == "C");
    }
}
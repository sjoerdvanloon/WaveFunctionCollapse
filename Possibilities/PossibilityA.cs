namespace WaveFunctionCollapse.Possibilities;

public class PossibilityA :IPossibility
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
        
        return allPossibilities.All(x => x.Name == "A" || x.Name == "Empty" || x.Name == "B");
    }
}
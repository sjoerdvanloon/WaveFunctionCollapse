namespace WaveFunctionCollapse.Possibilities;

public class PossibilityC :IPossibility
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
        
        return allPossibilities.All(x =>  x.Name == "B" || x.Name == "C");
    }
}
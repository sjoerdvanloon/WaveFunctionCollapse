namespace WaveFunctionCollapse.Possibilities;

public abstract class PossibilityBase
{
    protected abstract string[] GetExceptedNames();
    
    public bool IsPossible(ICellContext cellContext)
    {
        if (cellContext.HasPickedPossibility())
            throw new Exception("Cell already has a picked possibility");
        
        var neighbourContexts = cellContext.GetNeighbourContexts();

        if (!neighbourContexts.Any())
            return true; //  This is probably a 1x1 grid, so anything is possible

        var expectedNames = GetExceptedNames();
        foreach (var nbc in neighbourContexts)
        {
            if (nbc.LastPossibilities is null)
                continue; // This neighbour has no possibilities, so anything is possible
            
            var possibleNames = nbc.LastPossibilities.Select(x => x.Name).ToArray();
            if (!expectedNames.Any(x => possibleNames.Contains(x)))
                return false; // This neighbour has no possibilities that match the expected names

        }

        return true;
    }
}
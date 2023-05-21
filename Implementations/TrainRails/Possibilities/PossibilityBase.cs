using WaveFunctionCollapse.Grids;
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.TrainRails.Possibilities;

public abstract class PossibilityBase
{
    protected abstract string[] GetExceptedNames();
    public abstract Implementations.TrainRails.RailTypes RailType { get; }


    public bool IsPossible(ICellContext cellContext, Dictionary<Cell, CellContext> cellContexts)
    {
        if (cellContext.HasPickedPossibility())
            return false; // This cell already has a picked possibility

        var neighbours = cellContext.Cell.Neighbours.GetStraightNeighbours(); // Only look at straight neighbours

        if (!neighbours.Any())
            return true; //  This is probably a 1x1 grid, so anything is possible

        var expectedNames = GetExceptedNames();
        foreach (var neighbour in neighbours)
        {
            var neighbourCell = neighbour.Cell;
            if (!cellContexts.TryGetValue(neighbourCell, out var nbc))
                throw new Exception($"'{neighbourCell}' is not in the cellContexts dictionary");

            if (nbc.LastPossibilities is null)
                continue; // This neighbour has no possibilities, so anything is possible

            var possibleNames = nbc.LastPossibilities.Select(x => x.Name).ToArray();
            if (!expectedNames.Any(x => possibleNames.Contains(x)))
                return false; // This neighbour has no possibilities that match the expected names
        }

        return true;
    }
}
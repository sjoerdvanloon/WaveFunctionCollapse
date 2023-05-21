using WaveFunctionCollapse.Algorithm;
using WaveFunctionCollapse.Algorithm.InitialPossibilityGenerator;
using WaveFunctionCollapse.Algorithm.PossibilitySelectors;
using WaveFunctionCollapse.CellContents;
using WaveFunctionCollapse.Grids;
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.TrainRails;

public class TrainRailsSolver : SolverBase
{
    private readonly IPossibility[] _railTypePossibilities;

    public TrainRailsSolver(
        IPossibilitySelector possibilitySelector,
        ICellSelector cellSelector) : base(possibilitySelector, cellSelector)
    {
        // Get all enum values in a list
        var railTypePossibilities = 
            Enum.GetValues<RailTypes>()
                .Select(x=>new RailPossibility(x)).Cast<IPossibility>().ToArray();

        _railTypePossibilities = railTypePossibilities;
    }

    protected override IPossibility[] GetInitialPossibilities(Cell cell)
    {
        return _railTypePossibilities.ToArray();

    }
    
    protected override Neighbour[] GetApplicableNeighboursToUpdateEntropyFor(Cell cell)
    {
        return cell.Neighbours.GetStraightNeighbours();
    }

    protected override ICellContent GenerateErrorCellContent(Cell cell, CellContext cellContext)
    {
        return RailCellContent.CreateError("Not passed");
    }

    protected override ICellContent GeneratePickedValueCellContent(Cell cell, CellContext cellContext)
    {
        var possibility = cellContext.PickedPossibility as RailPossibility;
        if (possibility == null)
            throw new Exception($"Possibility is not of type {nameof(RailPossibility)}");
        var content = RailCellContent.CreatePicked(possibility.RailType);
        return content;
    }

    protected override ICellContent GenerateUndeterminedCellContent(Cell cell, CellContext cellContext)
    {
        if (!cellContext.LastPossibilities.Any())
            return RailCellContent.CreateError("No possibilities");

        var possibilities = cellContext.LastPossibilities.Cast<RailPossibility>().ToArray();
        var trainRails = possibilities.Select(x => x.RailType).ToArray();
        return RailCellContent.CreateUndetermined(trainRails);
    }
}
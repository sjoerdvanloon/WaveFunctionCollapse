using WaveFunctionCollapse.Algorithm;
using WaveFunctionCollapse.Algorithm.InitialPossibilityGenerator;
using WaveFunctionCollapse.Algorithm.PossibilitySelectors;
using WaveFunctionCollapse.CellContents;
using WaveFunctionCollapse.Grids;
using WaveFunctionCollapse.Implementations.TrainRails.Possibilities;
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.TrainRails;

public class TrainRailsSolver : SolverBase
{
    public TrainRailsSolver(IEnumerable<IPossibility> possibilities, IPossibilitySelector possibilitySelector,
        IInitialPossibilityGenerator initialPossibilityGenerator, ICellSelector cellSelector) : base(possibilities,
        possibilitySelector, initialPossibilityGenerator, cellSelector)
    {
    }

    protected override ICellContent GenerateErrorCellContent(Cell cell, CellContext cellContext)
    {
        return RailCellContent.CreateError("Not passed");
    }

    protected override ICellContent GeneratePickedValueCellContent(Cell cell, CellContext cellContext)
    {
        var possibility = cellContext.PickedPossibility as PossibilityBase;
        if (possibility == null)
            throw new Exception($"Possibility is not of type {nameof(PossibilityBase)}");
        var content = RailCellContent.CreatePicked(possibility.RailType);
        return content;
    }

    protected override ICellContent GenerateUndeterminedCellContent(Cell cell, CellContext cellContext)
    {
        if (cellContext.LastPossibilities == null || cellContext.LastPossibilities.Length == 0)
            return RailCellContent.CreateError("No possibilities");

        var possibilities = cellContext.LastPossibilities.Cast<PossibilityBase>().ToArray();
        var trainRails = possibilities.Select(x => x.RailType).ToArray();
        return RailCellContent.CreateUndetermined(trainRails);
    }
}
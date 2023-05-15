using WaveFunctionCollapse.Algorithm;
using WaveFunctionCollapse.Algorithm.InitialPossibilityGenerator;
using WaveFunctionCollapse.Algorithm.PossibilitySelectors;
using WaveFunctionCollapse.CellContents;
using WaveFunctionCollapse.Implementations.Letters.Possibilities;
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.Letters;

public class LetterSolver : SolverBase
{
    public LetterSolver(IEnumerable<IPossibility> possibilities, IPossibilitySelector possibilitySelector,
        IInitialPossibilityGenerator initialPossibilityGenerator, ICellSelector cellSelector) : base(possibilities,
        possibilitySelector, initialPossibilityGenerator, cellSelector)
    {
    }

    protected override ICellContent GenerateErrorCellContent(Cell cell, CellContext cellContext)
    {
        return LetterCellContent.CreateError("Not passed");
    }

    protected override ICellContent GeneratePickedValueCellContent(Cell cell, CellContext cellContext)
    {
        var possibility = cellContext.PickedPossibility as PossibilityBase;
        if (possibility == null)
            throw new Exception($"Possibility is not of type {nameof(PossibilityBase)}");
        var content = LetterCellContent.CreatePicked(possibility.Letter);
        return content;
    }

    protected override ICellContent GenerateUndeterminedCellContent(Cell cell, CellContext cellContext)
    {
        if (cellContext.LastPossibilities == null || cellContext.LastPossibilities.Length == 0)
            return LetterCellContent.CreateError("No possibilities");

        var possibilities = cellContext.LastPossibilities.Cast<PossibilityBase>().ToArray();
        var letters = possibilities.Select(x => x.Letter).ToArray();
        return LetterCellContent.CreateUndetermined(letters);
    }
}
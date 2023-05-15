using Polly;
using WaveFunctionCollapse.Algorithm.InitialPossibilityGenerator;
using WaveFunctionCollapse.Algorithm.PossibilitySelectors;
using WaveFunctionCollapse.CellContents;
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Algorithm;

public abstract class SolverBase : ISolver
{
    protected readonly IPossibility[] _possibilities;
    protected readonly IPossibilitySelector _possibilitySelector;
    protected readonly IInitialPossibilityGenerator _initialPossibilityGenerator;
    protected readonly ICellSelector _cellSelector;

    public SolverBase(IEnumerable<IPossibility> possibilities, IPossibilitySelector possibilitySelector,
        IInitialPossibilityGenerator initialPossibilityGenerator, ICellSelector cellSelector)
    {
        _possibilities = possibilities.ToArray();
        _possibilitySelector = possibilitySelector;
        _initialPossibilityGenerator = initialPossibilityGenerator;
        _cellSelector = cellSelector;
    }

    public bool Solve(Grid grid, int maxIterations = 1000)
    {
        var cells = grid.GetCells();
        var cellContextLookup = cells
            .ToDictionary(
                c => c,
                c => new CellContext(c)
                {
                    LastPossibilities = _initialPossibilityGenerator.GeneratePossibilities(c)
                });

        foreach (var cellContext in cellContextLookup.Values)
        {
            cellContext.Cell.UpdateCellContent(GenerateUndeterminedCellContent(cellContext.Cell, cellContext));
        }

        var timeoutPolicy = Policy.Timeout(TimeSpan.FromSeconds(10));

        timeoutPolicy.Execute(() =>
        {
            // while there are still steps to do
            for (int i = 0; i < maxIterations; i++)
            {
                if (!Iterate(cells, cellContextLookup))
                    break; // no more iterations possible
            }
        });

        return true;
    }

    private Cell[] FindCellsWithLowestEntropy(IEnumerable<CellContext> cells)
    {
        var cellContexts = cells as CellContext[] ?? cells.ToArray();
        if (!cellContexts.Any())
            return new Cell[] { };
        int lowestEntropy = cellContexts.Min(x => x.LastPossibilities?.Length ?? int.MaxValue);
        return cellContexts.Where(x => (x.LastPossibilities?.Length ?? int.MaxValue) == lowestEntropy)
            .Select(x => x.Cell)
            .ToArray();
    }

    protected abstract ICellContent GenerateErrorCellContent(Cell cell, CellContext cellContext);
    protected abstract ICellContent GeneratePickedValueCellContent(Cell cell, CellContext cellContext);
    protected abstract ICellContent GenerateUndeterminedCellContent(Cell cell, CellContext cellContext);

    private bool Iterate(IEnumerable<Cell> cells, Dictionary<Cell, CellContext> cellContextLookup)
    {
        var cellsWithEntropy = GenerateCellsWithEntropy(cellContextLookup.Values.ToArray());

        if (!cellsWithEntropy.Any())
            return false;

        // Find cells with lowest entropy
        Cell[] possibleCells = FindCellsWithLowestEntropy(cellContextLookup.Values);

        // Select cell with lowest entropy
        Cell bestCell = _cellSelector.SelectCell(possibleCells);

        // Get entropy of cell
        var lowestEntropy = cellsWithEntropy.Single(x => x.Cell == bestCell).Entropy;
        var entropyValue = lowestEntropy.ToString();

        // Get number of possibilities for the cell
        var cellContext = cellContextLookup[bestCell];
        var possibilities = _possibilities.Where(x => x.IsPossible(cellContext)).ToArray();
        cellContext.LastPossibilities = possibilities;

        // Get random possibility
        if (possibilities.Length == 0)
        {
            bestCell.UpdateCellContent(GenerateErrorCellContent(bestCell, cellContext));
            return false;
        }

        var possibility = _possibilitySelector.SelectOne(possibilities);
        cellContext.PickPossibility(possibility);
        bestCell.UpdateCellContent(GeneratePickedValueCellContent(bestCell, cellContext));

        // Update entropy of neighbours
        foreach (var neighbourContext in cellContext.GetNeighbourContexts())
        {
            if (neighbourContext.HasPickedPossibility())
                continue;

            var neighbourPossibilities = _possibilities.Where(x => x.IsPossible(neighbourContext)).ToArray();
            neighbourContext.LastPossibilities = neighbourPossibilities;

            neighbourContext.Cell.UpdateCellContent(GenerateUndeterminedCellContent(neighbourContext.Cell,
                neighbourContext));
        }

        return true;
    }

    private static EntropisedCell[] GenerateCellsWithEntropy(IEnumerable<CellContext> cellsWithContext)
    {
        var cellsWithEntropy = cellsWithContext
            .Where(x => !x.HasPickedPossibility())
            .Select(c =>
            {
                var entropy = c.LastPossibilities?.Length ?? int.MaxValue;
                return new EntropisedCell(c.Cell, entropy);
            }).ToArray();
        return cellsWithEntropy;
    }
}
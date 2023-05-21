using Polly;
using WaveFunctionCollapse.Algorithm.InitialPossibilityGenerator;
using WaveFunctionCollapse.Algorithm.PossibilitySelectors;
using WaveFunctionCollapse.CellContents;
using WaveFunctionCollapse.Grids;
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
            var initialContent = GenerateUndeterminedCellContent(cellContext.Cell, cellContext);
            cellContext.Cell.SetCellContent(initialContent);
        }

        var timeoutPolicy = Policy.Timeout(TimeSpan.FromSeconds(10));

        timeoutPolicy.Execute(() =>
        {
            // while there are still steps to do
            for (int i = 0; i < maxIterations; i++)
            {
                var state = Iterate( cellContextLookup);
                if ( state is IterationState.Fail or IterationState.Done)
                    break; // no more iterations possible
            }
        });

        return true;
    }

    public enum IterationState
    {
        Continue,
        Done,
        Fail
    }

   

    protected abstract ICellContent GenerateErrorCellContent(Cell cell, CellContext cellContext);
    protected abstract ICellContent GeneratePickedValueCellContent(Cell cell, CellContext cellContext);
    protected abstract ICellContent GenerateUndeterminedCellContent(Cell cell, CellContext cellContext);

    private IterationState Iterate(Dictionary<Cell, CellContext> cellContextLookup)
    {
        // Find cells with lowest entropy
        Cell[] possibleCells = FindCellsWithLowestEntropy(cellContextLookup.Values);

        if (!possibleCells.Any())
            return  IterationState.Done;

        // Select cell with lowest entropy
        Cell bestCell = _cellSelector.SelectCell(possibleCells);

        // Get number of possibilities for the cell
        var cellContext = cellContextLookup[bestCell];
        var possibilities = _possibilities.Where(x => x.IsPossible(cellContext, cellContextLookup)).ToArray();
        cellContext.LastPossibilities = possibilities;

        // Get random possibility
        if (possibilities.Length == 0)
        {
            bestCell.SetCellContent(GenerateErrorCellContent(bestCell, cellContext));
            return  IterationState.Fail;
        }

        var possibility = _possibilitySelector.SelectOne(possibilities);
        cellContext.PickPossibility(possibility);
        bestCell.SetCellContent(GeneratePickedValueCellContent(bestCell, cellContext));

        // Update entropy of neighbours
        var neighbours = cellContext.Cell.Neighbours;
        // TODO: this could be algorithm specific
        var neighbourContexts = neighbours.GetDirectNeighbours().Select(x => cellContextLookup[x.Cell]).ToArray(); // All neighbours could be possible
        foreach (var neighbourContext in neighbourContexts)
        {
            if (neighbourContext.HasPickedPossibility())
                continue;

            var neighbourPossibilities = _possibilities.Where(x => x.IsPossible(neighbourContext, cellContextLookup)).ToArray();
            neighbourContext.LastPossibilities = neighbourPossibilities;

            neighbourContext.Cell.SetCellContent(GenerateUndeterminedCellContent(neighbourContext.Cell,
                neighbourContext));
        }

        return IterationState.Continue;
    }
    
    private Cell[] FindCellsWithLowestEntropy(IEnumerable<CellContext> cellContexts)
    {
        var onlyUnfinishedCells = cellContexts
            .Where(x => !x.HasPickedPossibility()).ToArray();
        
        if (!onlyUnfinishedCells.Any())
            return new Cell[] { }; // No cells, return empty array
        
        // Get lowest number
        int lowestEntropy = onlyUnfinishedCells.Min(x => x.LastPossibilities?.Length ?? int.MaxValue);
        
        // Get all cells with the lowest umber
        return onlyUnfinishedCells.Where(x =>  (x.LastPossibilities?.Length ?? int.MaxValue) == lowestEntropy)
            .Select(x => x.Cell)
            .ToArray();
    }
}
using Polly;
using Polly.Timeout;
using WaveFunctionCollapse.CellContents;
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Algorithm;

public class GridAutoFiller
{
    private readonly ILowestEntropyCellFinder _lowestEntropyCellFinder;
    private readonly ICellSelector _cellSelector;
    private readonly CellContextGenerator _cellContextGenerator;
    private readonly IPossibilitySelector _possibilitySelector;
    private readonly IPossibility[] _possibilities;

    public GridAutoFiller(
        ILowestEntropyCellFinder lowestEntropyCellFinder,
        ICellSelector cellSelector,
        IEnumerable<IPossibility> possibilities,
        CellContextGenerator cellContextGenerator,
        IPossibilitySelector possibilitySelector)
    {
        _lowestEntropyCellFinder = lowestEntropyCellFinder;
        _cellSelector = cellSelector;
        _cellContextGenerator = cellContextGenerator;
        _possibilitySelector = possibilitySelector;
        _possibilities = possibilities.ToArray();
    }


    public void FillGrid(Grid grid, int steps = 1)
    {
        var cellsWithContext = _cellContextGenerator.FromGrid(grid);

        var timeoutPolicy =  Policy.Timeout(TimeSpan.FromSeconds(10));
        
        timeoutPolicy.Execute(() =>
        {
            // while there are still steps to do
            for (int i = 0; i < steps; i++)
            {
                if (!Iteration(cellsWithContext))
                {
                    break; // no more iterations possible
                }

                ;
            }
        });
    }

    private bool Iteration(CellContext[] cellsWithContext)
    {
        var cellsWithEntropy = GenerateCellsWithEntropy(cellsWithContext);
        
        if (cellsWithEntropy.Length == 0)
        {
            return false;
        }

        // Find cells with lowest entropy
        Cell[] possibleCells = _lowestEntropyCellFinder.FindLowestEntropyCells(cellsWithEntropy);

        // Select cell with lowest entropy
        Cell bestCell = _cellSelector.SelectCell(possibleCells);

        // Get entropy of cell
        var lowestEntropy = cellsWithEntropy.Single(x => x.Cell == bestCell).Entropy;
        var entropyValue = lowestEntropy.ToString();

        // Get number of possibilities for the cell
        var cellContext = cellsWithContext.Single(x => x.Cell == bestCell);
        var possibilities = _possibilities.Where(x => x.IsPossible(cellContext)).ToArray();
        cellContext.LastPossibilities = possibilities;

        // Get random possibility
        if (possibilities.Length == 0)
        {
            bestCell.UpdateCellContent(new TextOnlyCellContent("X"));
            return false;
        }
        
        var possibility = _possibilitySelector.SelectOne(possibilities);
        cellContext.PickPossibility(possibility);
        bestCell.UpdateCellContent(new TextOnlyCellContent(possibility.Name));

        // Update entropy of neighbours
        foreach (var neighbourContext in cellContext.GetNeighbourContexts())
        {
            if (neighbourContext.HasPickedPossibility())
                continue;
            
            var neighbourPossibilities = _possibilities.Where(x => x.IsPossible(neighbourContext)).ToArray();
            neighbourContext.LastPossibilities = neighbourPossibilities;

            var possibiltyNames = neighbourPossibilities.Select(x => x.Name).ToArray();
            var neightbourCellContentValue = "N/A";

            if (possibiltyNames.Any())
                neightbourCellContentValue = string.Join(",", possibiltyNames)
            ;
            
            neighbourContext.Cell.UpdateCellContent(new TextOnlyCellContent(neightbourCellContentValue));
        }

        return true;
    }

    private static EntropisedCell[] GenerateCellsWithEntropy(CellContext[] cellsWithContext)
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
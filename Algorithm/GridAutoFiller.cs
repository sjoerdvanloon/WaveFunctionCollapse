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

        var cellsWithEntropy = cellsWithContext.Select(c =>
        {
            var entropy = c.LastPossibilities?.Length ?? int.MaxValue;
            return new EntropisedCell(c.Cell, entropy);
        }).ToArray();


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

        // Get random possibility
        var possibility = _possibilitySelector.SelectOne(possibilities);
        
        var value = possibility.Name;
        
        bestCell.UpdateCellContent(new TextOnlyCellContent(value));
    }
}
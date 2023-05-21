using System.Diagnostics;
using WaveFunctionCollapse.Algorithm;
using WaveFunctionCollapse.Algorithm.PossibilitySelectors;
using WaveFunctionCollapse.CellContents;
using WaveFunctionCollapse.Grids;
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.Sudoku;

public class SudokuSolver : SolverBase
{
    private readonly IPossibility[] _allPosibilities;
    public SudokuSolver(IPossibilitySelector possibilitySelector, ICellSelector cellSelector) : base(possibilitySelector, cellSelector)
    {
        // Generate range from 1 till 9
        _allPosibilities = Enumerable.Range(1, 9).Select(x=> new SudokuCellPossibility(x)).ToArray();
    }

    protected override IPossibility[] GetInitialPossibilities(Cell cell)
    {
        if (cell.CellContent is SudokuCellContent sudokuCellContent)
        {
            var cellPossibilities = sudokuCellContent.Numbers!.Select(x=> _allPosibilities[x-1]).ToArray();
            Debug.Assert(cellPossibilities.Length>0);
            return cellPossibilities;
        }
        else
        {
            return _allPosibilities.ToArray();

        }
    }

    protected override ICellContent GenerateErrorCellContent(Cell cell, CellContext cellContext)
    {
        return SudokuCellContent.CreateError("Error");
    }

    protected override ICellContent GeneratePickedValueCellContent(Cell cell, CellContext cellContext)
    {
        var possibility = cellContext.PickedPossibility as SudokuCellPossibility;
        if (possibility == null)
            throw new Exception($"Possibility is not of type {nameof(SudokuCellPossibility)}");
        var content = SudokuCellContent.CreatePicked(possibility.Number);
        return content;
    }

    protected override ICellContent GenerateUndeterminedCellContent(Cell cell, CellContext cellContext)
    {
        if (!cellContext.LastPossibilities.Any())
            return SudokuCellContent.CreateError("No possibilities");

        var possibilities = cellContext.LastPossibilities.Cast<SudokuCellContent>().ToArray();
        var numbers = possibilities.SelectMany(x => x.Numbers).ToArray();
        return SudokuCellContent.CreateUndetermined(numbers);
    }

    protected override Neighbour[] GetApplicableNeighboursToUpdateEntropyFor(Cell cell)
    {
        // All neighbours in the same block of 3x3 in a 9x9 grid
        
        // All neighbours in the same row
        
        // All neighbours in the same column
        
        return cell.Neighbours.GetStraightNeighbours();
    }
}
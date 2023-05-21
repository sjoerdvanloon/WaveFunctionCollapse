using System.Diagnostics;
using WaveFunctionCollapse.Grids;
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.Sudoku;

public class SudokuCellPossibility : IPossibility
{
    public int Number { get; }
  
   
    public SudokuCellPossibility(int number)
    {
        Number = number;
    }


    public string Name => GetText();

    public string GetText()
    {
        return Number.ToString();
    }

    public bool IsPossible(ICellContext cellContext, Dictionary<Cell, CellContext> cellContexts)
    {
        if (cellContext.HasPickedPossibility)
            return false; // This cell already has a picked possibility

        var neighbours = cellContext.Cell.Neighbours.GetStraightNeighbours(); // Only look at straight neighbours

        if (!neighbours.Any())
            return true; //  This is probably a 1x1 grid, so anything is possible

        return true;
    }

   

}
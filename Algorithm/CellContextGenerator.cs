using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Algorithm;

public class CellContextGenerator
{
    public CellContext[] FromGrid(Grid grid)
    {
        var cellsWithContext = grid.GetCells().Select(x => new CellContext(x)).ToArray();

        // Loop through grid to set neighbours
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                var cell = grid.GetCell(x, y);
                var cellContext = cellsWithContext.Single(x => x.Cell == cell);

                var neighbours = new List<CellContext>();

                // Get left neighbour
                if (x > 0)
                {
                    var leftNeighbour = grid.GetCell(x - 1, y);
                    var leftNeighbourContext = cellsWithContext.Single(x => x.Cell == leftNeighbour);
                    neighbours.Add(leftNeighbourContext);
                }

                // Get right neighbour
                if (x < grid.Width - 1)
                {
                    var rightNeighbour = grid.GetCell(x + 1, y);
                    var rightNeighbourContext = cellsWithContext.Single(x => x.Cell == rightNeighbour);
                    neighbours.Add(rightNeighbourContext);
                }

                // Get top neighbour
                if (y > 0)
                {
                    var topNeighbour = grid.GetCell(x, y - 1);
                    var topNeighbourContext = cellsWithContext.Single(x => x.Cell == topNeighbour);
                    neighbours.Add(topNeighbourContext);
                }

                // Get bottom neighbour
                if (y < grid.Height - 1)
                {
                    var bottomNeighbour = grid.GetCell(x, y + 1);
                    var bottomNeighbourContext = cellsWithContext.Single(x => x.Cell == bottomNeighbour);
                    neighbours.Add(bottomNeighbourContext);
                }

                cellContext.SetNeighbours(neighbours.ToArray());
            }
        }

        return cellsWithContext;
    }
}
namespace WaveFunctionCollapse.Grids;

public class NeighbourGenerator
{
    // public Neighbours[] ForGrid(Grid grid)
    // {
    //     var neighbours = new List<Neighbours>();
    //     for (int y = 0; y < grid.Height; y++)
    //     {
    //         for (int x = 0; x < grid.Width; x++)
    //         {
    //             var cell = grid.GetCellBasedOnCoordinates(x, y);
    //             var cellNeighbours = ForCell(cell, grid);
    //             neighbours.Add(cellNeighbours);
    //         }
    //     }
    //
    //     return neighbours.ToArray();
    // }

    public Neighbours ForCell(Cell cell, Grid grid)
    {
        // initialize neighbours
        var neighbours = new List<Neighbour>();

        var x = cell.XPosition;
        var y = cell.YPosition;

        var hasLeftNeighbour = x > 0;
        var hasRightNeighbour = x < grid.Width - 1;
        var hasTopNeighbour = y > 0;
        var hasBottomNeighbour = y < grid.Height - 1;

        if (hasLeftNeighbour)
        {
            var leftNeighbour = grid.GetCellBasedOnCoordinates(x-1, y );
            neighbours.Add(new Neighbour(cell, NeighbourDirection.Left, leftNeighbour));
        }

        if (hasRightNeighbour)
        {
            var rightNeighbour = grid.GetCellBasedOnCoordinates(x+1, y );
            neighbours.Add(new Neighbour(cell, NeighbourDirection.Right, rightNeighbour));
        }

        if (hasTopNeighbour)
        {
            var topNeighbour = grid.GetCellBasedOnCoordinates(x , y-1);
            neighbours.Add(new Neighbour(cell, NeighbourDirection.Top, topNeighbour));
        }

        if (hasBottomNeighbour)
        {
            var bottomNeighbour = grid.GetCellBasedOnCoordinates(x , y+1);
            neighbours.Add(new Neighbour(cell, NeighbourDirection.Bottom, bottomNeighbour));
        }

        if (hasLeftNeighbour && hasTopNeighbour)
        {
            var topLeftNeighbour = grid.GetCellBasedOnCoordinates(x - 1, y - 1);
            neighbours.Add(new Neighbour(cell, NeighbourDirection.TopLeft, topLeftNeighbour));
        }

        if (hasLeftNeighbour && hasBottomNeighbour)
        {
            var bottomLeftNeighbour = grid.GetCellBasedOnCoordinates(x - 1, y + 1);
            neighbours.Add(new Neighbour(cell, NeighbourDirection.BottomLeft, bottomLeftNeighbour));
        }

        if (hasRightNeighbour && hasTopNeighbour)
        {
            var topRightNeighbour = grid.GetCellBasedOnCoordinates(x + 1, y - 1);
            neighbours.Add(new Neighbour(cell, NeighbourDirection.TopRight, topRightNeighbour));
        }

        if (hasRightNeighbour && hasBottomNeighbour)
        {
            var bottomRightNeighbour = grid.GetCellBasedOnCoordinates(x + 1, y + 1);
            neighbours.Add(new Neighbour(cell, NeighbourDirection.BottomRight, bottomRightNeighbour));
        }

        return new Neighbours(cell, neighbours);
    }
}
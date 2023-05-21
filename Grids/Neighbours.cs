namespace WaveFunctionCollapse.Grids;

public class Neighbours
{
    private readonly List<Neighbour> _neighbours;
    private readonly Cell _cell;

    public Cell Cell => _cell;

    public Neighbours(Cell fromCell, IEnumerable<Neighbour> neighbours)
    {
        _cell = fromCell;
        _neighbours = neighbours.ToList();
    }
    
    public void AddNeighbour(Cell neighbourCell, NeighbourDirection neighbourDirection)
    {
        if (_neighbours.Any(x => x.Direction == neighbourDirection))
            throw new InvalidOperationException("Neighbour already exists");
        
        var neighbour = new Neighbour(_cell, neighbourDirection, neighbourCell);
        _neighbours.Add(neighbour);
    }
   
    
    public Neighbour[] GetDirectNeighbours()
    {
        return _neighbours.ToArray();
    }
    
    public Neighbour[] GetDiagonalNeighbours()
    {
        return _neighbours.Where(x=> x.Direction == NeighbourDirection.BottomLeft || x.Direction == NeighbourDirection.BottomRight || x.Direction == NeighbourDirection.TopLeft || x.Direction == NeighbourDirection.TopRight).ToArray();
    }
    
    public Neighbour[] GetStraightNeighbours()
    {
        return _neighbours.Where(x=> x.Direction == NeighbourDirection.Bottom || x.Direction == NeighbourDirection.Right || x.Direction == NeighbourDirection.Top || x.Direction == NeighbourDirection.Left).ToArray();

    }
    
//     public Cell[] GetAllNeighboursInDirection(Cell cell, params NeighbourDirection[] neighbourDirections)
//     {
//         if (cell == null) throw new ArgumentNullException(nameof(cell));
//         if (neighbourDirections == null) throw new ArgumentNullException(nameof(neighbourDirections));
//         if (!neighbourDirections.Any())
//             throw new ArgumentException($"No directions specfified for {nameof(neighbourDirections)}");
//         
// var neightbourCells = new List<Cell>();
//         foreach (var direction in neighbourDirections)
//         {
//             var currentCell = cell;
//             while(currentCell != null)
//             {
//                 var neighbours = currentCell.
//                 var neighbour = .SingleOrDefault(x => x.Direction == direction && x.FromCell == currentCell);
//                 if (neighbour is null)
//                     break;
//                 neightbourCells.Add( neighbour.Cell);
//                 currentCell = neighbour.Cell;
//             }
//         }
//     }
    
}
using System.Diagnostics;
using WaveFunctionCollapse.Grids;
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.TrainRails;

public class RailPossibility : IPossibility, IWeighted
{
    public RailTypes RailType { get; }
    public float Weight => _weights[RailType];

    private readonly Dictionary<RailTypes, float> _weights = new()
    {
        {RailTypes.Empty, 8},
        {RailTypes.StraightHorizontal, 2},
        {RailTypes.StraightVertical, 2},
        {RailTypes.Cross, 0.5f},
        {RailTypes.StraightVerticalWithLeftJunction, 0.5f},
        {RailTypes.StraightVerticalWithRightJunction, 0.5f},
        {RailTypes.StraightHorizontalWithTopJunction, 0.5f},
        {RailTypes.StraightHorizontalWithBottomJunction, 0.5f},
    };

    public RailPossibility(RailTypes railType)
    {
        RailType = railType;
    }


    public string Name => GetText();

    public string GetText()
    {
        return RailType.ToString();
    }

    public bool IsPossible(ICellContext cellContext, Dictionary<Cell, CellContext> cellContexts)
    {
        if (cellContext.HasPickedPossibility)
            return false; // This cell already has a picked possibility

        var neighbours = cellContext.Cell.Neighbours.GetStraightNeighbours(); // Only look at straight neighbours

        if (!neighbours.Any())
            return true; //  This is probably a 1x1 grid, so anything is possible

        var neededEndPoints = RailType.GetEndPoints();

        // North 
        if (!DoesNeighbourHaveAnyValidPossibilities(EndPoints.North, NeighbourDirection.Top, cellContexts, neededEndPoints, neighbours)) 
            return false;
        if (!DoesNeighbourHaveAnyValidPossibilities(EndPoints.South, NeighbourDirection.Bottom, cellContexts, neededEndPoints, neighbours)) 
            return false;
        if (!DoesNeighbourHaveAnyValidPossibilities(EndPoints.West, NeighbourDirection.Left, cellContexts, neededEndPoints, neighbours)) 
            return false;
        if (!DoesNeighbourHaveAnyValidPossibilities(EndPoints.East, NeighbourDirection.Right, cellContexts, neededEndPoints, neighbours))
            return false;

        return true;
    }

    private static bool DoesNeighbourHaveAnyValidPossibilities(
        EndPoints applicableEndPoint,
        NeighbourDirection neighbourDirection,  
        Dictionary<Cell, CellContext> cellContexts,
        EndPoints[] neededEndPoints,
        Neighbour[] neighbours)
    {
        var doesNeighbourNeedEndPoint = neededEndPoints.Contains(applicableEndPoint);
        var neighbourNeededEndpoint = applicableEndPoint.GetOpposite();

        var northNeighbour = neighbours.SingleOrDefault(x => x.Direction == neighbourDirection);
        if (northNeighbour is not null)
        {
            var neighbourCellContext = cellContexts[northNeighbour.Cell];
            var neighbourRailTypes = neighbourCellContext.LastPossibilities
                .Select(x => ((RailPossibility)x).RailType)
                .ToArray();

            Debug.Assert(neighbourRailTypes.Any());

            foreach (var neighbourPossibleRailType in neighbourRailTypes)
            {
                var neighbourPossibleEndPoints = neighbourPossibleRailType.GetEndPoints();
                var doesAnyHaveTheNeededEndPoint = neighbourPossibleEndPoints.Any(x => x == neighbourNeededEndpoint);
                if (doesNeighbourNeedEndPoint && doesAnyHaveTheNeededEndPoint ||
                    !doesNeighbourNeedEndPoint && !doesAnyHaveTheNeededEndPoint)
                {
                    return true;
                }
            }

            return false;
        }

        return true;
    }

}
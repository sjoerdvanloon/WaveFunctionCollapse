namespace WaveFunctionCollapse.Implementations.TrainRails;

public static class EndPointExtensions
{
    public static EndPoints[] GetEndPoints(this RailTypes railType)
    {
        switch (railType)
        {
            case RailTypes.Empty:
                return new EndPoints[0];
            case RailTypes.StraightHorizontal:
                return new EndPoints[]{ EndPoints.East, EndPoints.West };
            case RailTypes.StraightVertical:
                return new EndPoints[]{ EndPoints.North, EndPoints.South };
            case RailTypes.Cross:
                return new EndPoints[]{ EndPoints.North, EndPoints.South, EndPoints.East, EndPoints.West };
            case RailTypes.StraightVerticalWithLeftJunction:
                return new EndPoints[]{ EndPoints.North, EndPoints.South,  EndPoints.West };
            case RailTypes.StraightVerticalWithRightJunction:
                return new EndPoints[]{ EndPoints.North, EndPoints.South, EndPoints.East };
            // case RailTypes.StraightHorizontalWithTopJunction:
            //     return new EndPoints[]{ EndPoints.North, EndPoints.East, EndPoints.West };
            // case RailTypes.StraightHorizontalWithBottomJunction:
            //     return new EndPoints[]{ EndPoints.South, EndPoints.East, EndPoints.West };
            default:
                throw new ArgumentOutOfRangeException(nameof(railType), railType, null);
        }
    }
    public  static EndPoints GetOpposite(this EndPoints endPoint)
    {
        return endPoint switch
        {
            EndPoints.North => EndPoints.South,
            EndPoints.South => EndPoints.North,
            EndPoints.West => EndPoints.East,
            EndPoints.East => EndPoints.West,
            _ => throw new ArgumentOutOfRangeException(nameof(endPoint), endPoint, "Cannot found opposite for this EndPoint")
        };
    }
}
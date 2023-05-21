namespace WaveFunctionCollapse.Implementations.TrainRails;

public static class RailTypeExtensions
{
    public static string ToEmbeddedResourceName(this RailTypes railType)
    {
        switch (railType)
        {
            case RailTypes.Empty:
                return "blank";
            case RailTypes.StraightVerticalWithLeftJunction:
                return "left";
            case RailTypes.StraightVerticalWithRightJunction:
                return "right";
            case RailTypes.StraightHorizontalWithTopJunction:
                return "up";
            case RailTypes.StraightHorizontalWithBottomJunction:
                return "down";
            case RailTypes.Cross:
                return "cross";
            case RailTypes.StraightHorizontal:
                return "horizontal";
            case RailTypes.StraightVertical:
                return "vertical";
            default:
                throw new ArgumentOutOfRangeException(nameof(railType), railType, null);
        }
    }
    
    public static string ToAsciiArtString(this RailTypes railType)
    {
        switch (railType)
        {
            case RailTypes.Empty:
                return "[]";
            case RailTypes.StraightHorizontal:
                return "--";
            case RailTypes.StraightVertical:
                return "|";
            case RailTypes.Cross:
                return "-|-";
            case RailTypes.StraightVerticalWithLeftJunction:
                return "-|";
            case RailTypes.StraightVerticalWithRightJunction:
                return "|-";
            case RailTypes.StraightHorizontalWithTopJunction:
                return "_|_";
            case RailTypes.StraightHorizontalWithBottomJunction:
                return "`|`";
            default:
                throw new ArgumentOutOfRangeException(nameof(railType), railType, null);
        }
    }
}
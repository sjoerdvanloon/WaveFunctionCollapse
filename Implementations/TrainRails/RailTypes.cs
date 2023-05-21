namespace WaveFunctionCollapse.Implementations.TrainRails;

public enum RailTypes
{
    Empty,
    StraightHorizontal,
    StraightVertical,
    Cross,
    StraightVerticalWithLeftJunction,
    StraightVerticalWithRightJunction,
    // StraightHorizontalWithTopJunction,
    // StraightHorizontalWithBottomJunction
    // EndLeft,
    // EndRight,
    // EndTop,
    // EndBottom,
}

public class RailTypeDefinition
{
    
}

public static class RailTypeExtensions
{
    public static string ToASCIIArtString(this RailTypes railType)
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
            // case RailTypes.StraightHorizontalWithTopJunction:
            //     return "" 
            // case RailTypes.StraightHorizontalWithBottomJunction:
            //     break;
            default:
                throw new ArgumentOutOfRangeException(nameof(railType), railType, null);
        }
    }
}
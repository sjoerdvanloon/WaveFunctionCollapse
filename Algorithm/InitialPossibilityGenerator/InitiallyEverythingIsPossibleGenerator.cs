using WaveFunctionCollapse.Grids;
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Algorithm.InitialPossibilityGenerator;

public class InitiallyEverythingIsPossibleGenerator : IInitialPossibilityGenerator
{
    private readonly IPossibility[] _possibilities;

    public InitiallyEverythingIsPossibleGenerator(IEnumerable<IPossibility> possibilities)
    {
        if (possibilities == null)
            throw new ArgumentNullException(nameof(possibilities));
        var enumerable = possibilities as IPossibility[] ?? possibilities.ToArray();
        if (!enumerable.Any())
            throw new ArgumentException($"No possibilities provided", nameof(possibilities));

        _possibilities = enumerable;
    }

    public IPossibility[] GeneratePossibilities(Cell cell)
    {
        return _possibilities.ToArray();
    }
}
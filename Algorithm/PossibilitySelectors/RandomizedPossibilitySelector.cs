using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Algorithm.PossibilitySelectors;

public class RandomizedPossibilitySelector : IPossibilitySelector
{
    private readonly Random _random;

    public RandomizedPossibilitySelector(Random random)
    {
        _random = random;
    }

    public IPossibility SelectOne(IPossibility[] possibilities)
    {
        var index = _random.Next(0, possibilities.Length);
        return possibilities[index];
    }
}
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Algorithm;

public interface IPossibilitySelector
{
    IPossibility SelectOne(IPossibility[] possibilities);
}

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
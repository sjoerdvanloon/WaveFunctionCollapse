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
        var weightedBag = new WeightedRandomBag<IPossibility>(_random);
        foreach (var possibility in possibilities)
        {
            if (possibility is IWeighted weighted)
            {
                weightedBag.AddEntry(possibility, weighted.Weight);
            }
            else
            {
                weightedBag.AddEntry(possibility, 1);
            }
        }

        return   weightedBag.GetRandom();
    }
}
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Algorithm.PossibilitySelectors;

public interface IPossibilitySelector
{
    IPossibility SelectOne(IPossibility[] possibilities);
}
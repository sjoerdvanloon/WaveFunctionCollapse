using WaveFunctionCollapse.Algorithm.InitialPossibilityGenerator;
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Implementations.Letters;

public class InitialPossibilityGenerator : IInitialPossibilityGenerator
{
    private readonly IEnumerable<IPossibility> _possibilities;

    public InitialPossibilityGenerator(IEnumerable<IPossibility> possibilities)
    {
        _possibilities = possibilities;
    }
    
    public IPossibility[] GeneratePossibilities(Cell cell)
    {
        return  _possibilities.ToArray();
    }
}
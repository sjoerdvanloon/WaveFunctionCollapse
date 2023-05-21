using WaveFunctionCollapse.Grids;
using WaveFunctionCollapse.Possibilities;

namespace WaveFunctionCollapse.Algorithm.InitialPossibilityGenerator;

public interface IInitialPossibilityGenerator
{
    IPossibility[] GeneratePossibilities(Cell cell);
}
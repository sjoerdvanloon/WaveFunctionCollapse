using ConsoleTables;
using WaveFunctionCollapse.Grids;

namespace WaveFunctionCollapse.Renderers;

public class ConsoleGridRenderer : IGridRenderer
{
    private readonly Action<string> _writeLine;

    public ConsoleGridRenderer(Action<string> writeLine)
    {
        _writeLine = writeLine;
    }

    public void DrawGrid(Grid grid)
    {
        // Get data
        var cells = grid.GetCells();

        // Generate headers
        var headers =
            Enumerable.Range(0, grid.Width).Select(x => x.ToString()).ToArray();

        // CreateBasedOnCoordinates table
        var table = new ConsoleTable(new string[1] { " Y/X " }.Concat(headers).ToArray());

        var cellRenderer = new ConsoleCellRenderer(table);

        for (int y = 0; y < grid.Height; y++)
        {
            var rowValues = new object[grid.Width + 1];
            rowValues[0] = y.ToString();
            table.AddRow(rowValues);
            for (int x = 0; x < grid.Width; x++)
            {
                var cellIndex = y * grid.Width + x;
                var cell = cells[cellIndex];
                cellRenderer.Render(cell);
            }
        }

        _writeLine(table.ToString());
    }
}
using System.Text;
using ConsoleTables;

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

        // Create table
        var table = new ConsoleTable(new string[1] { " " }.Concat(headers).ToArray());

        var cellRenderer = new ConsoleCellRenderer(table);

        for (int x = 0; x < grid.Height; x++)
        {
            object[] rowValues = new string[grid.Width + 1];
            rowValues[0] = x.ToString();
            table.AddRow(rowValues);

            for (int y = 0; y < grid.Width; y++)
            {
                var cellIndex = y + x * grid.Width;
                var cell = cells[cellIndex];
                cellRenderer.Render(cell);
            }
        }

        _writeLine(table.ToString());
    }
}
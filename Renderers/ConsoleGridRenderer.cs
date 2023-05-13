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
        var table = new ConsoleTable(new string[1] {" "}.Concat(headers).ToArray() );
        for (int y = 0; y < grid.Height; y++)
        {
            object[] rowValues = new string[grid.Width+1];
            rowValues[0] = y.ToString();
            
            for (int x = 0; x < grid.Width; x++)
            {
                var cellIndex = y * grid.Width + x;
                var cell = cells[cellIndex];
                rowValues[x+1] = cell.Text;
                // Get cell index
                
                // sb.Append(cell.Text);
                // sb.Append("|");
                //_writeLine($" {cell.Text} ");
            }

            table.AddRow(rowValues);
        }


        _writeLine(table.ToString());

       
            // var sb = new StringBuilder();
            // sb.Append("|");
            // for (int x = 0; x < grid.Width; x++)
            // {
            //     // Get cell index
            //     var cellIndex = y * grid.Width + x;
            //     var cell = cells[cellIndex];
            //     sb.Append(cell.Text);
            //     sb.Append("|");
            //     //_writeLine($" {cell.Text} ");
            // }
            //
            // // sb.Append("|");
            // _writeLine(sb.ToString());
       // }
    }
}
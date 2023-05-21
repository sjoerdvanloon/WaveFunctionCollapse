using ConsoleTables;
using WaveFunctionCollapse.Grids;

namespace WaveFunctionCollapse.Renderers;

public class ConsoleCellRenderer : ICellRenderer
{
    private readonly ConsoleTable _table;

    public ConsoleCellRenderer(ConsoleTable table)
    {
        _table = table;
    }
        
    public void Render(Cell cell)
    {
        var value = cell.CellContent.GetFriendlyText();
        
        if (cell.Row > _table.Rows.Count )
            throw new Exception(
                $"Row {cell.Row} is out of range of the table with {_table.Rows.Count} rows.");
        var row = _table.Rows[cell.Row-1];

        if (cell.Column > row.Length   )
            throw new Exception(
                $"Column {cell.Column} is out of range of the table with {row.Length} column in row {cell.Row}.");
        row[cell.Column] = value;

    }
}
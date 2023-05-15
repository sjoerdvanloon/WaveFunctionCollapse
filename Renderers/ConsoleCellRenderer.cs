using ConsoleTables;

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
        
        var xPositionInTable = cell.XPosition;
        if (_table.Rows.Count <= xPositionInTable)
            throw new Exception(
                $"X position {xPositionInTable} is out of range of the table with {_table.Rows.Count} rows.");
        var row = _table.Rows[xPositionInTable];

        var yPositionInTable = cell.YPosition + 1;
        if (row.Length <= yPositionInTable)
            throw new Exception(
                $"Y position {yPositionInTable} is out of range of the table with {row.Length} column in row {xPositionInTable}.");
        row[yPositionInTable] = cell.CellContent.GetFriendlyText();

    }
}
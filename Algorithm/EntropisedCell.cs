namespace WaveFunctionCollapse.Algorithm;

public class EntropisedCell
{
    public  Cell Cell { get; }
    public int Entropy { get; set; }
   
    public EntropisedCell(Cell cell, int entropy = int.MaxValue)
    {
        Cell = cell;
        Entropy = entropy;
    }
    
    public override string ToString()
    {
        return $"{Cell} with entropy {Entropy}";
    }



 
}
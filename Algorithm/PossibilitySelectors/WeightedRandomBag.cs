namespace WaveFunctionCollapse.Algorithm.PossibilitySelectors;

public class WeightedRandomBag<T>  {

    private struct Entry {
        public double AccumulatedWeight;
        public T Item;
    }

    private readonly List<Entry> _entries = new List<Entry>();
    private double _accumulatedWeight;
    private readonly Random _rand;
    
    public WeightedRandomBag(Random rand) {
        this._rand = rand;
    }

    public void AddEntry(T item, float weight) {
        _accumulatedWeight += weight;
        _entries.Add(new Entry { Item = item, AccumulatedWeight = _accumulatedWeight });
    }

    public T GetRandom() {
        double r = _rand.NextDouble() * _accumulatedWeight;

        foreach (Entry entry in _entries) {
            if (entry.AccumulatedWeight >= r) {
                return entry.Item;
            }
        }

        throw new Exception($"No entries in {nameof(WeightedRandomBag<T>)}");
    }
}
namespace ArtifactGeneratorLibrary
{
    /// <summary>
    /// a model made to hold infomation about one simulation
    /// </summary>
    public class Simulation
    {
        
        public int Index { get; set; }
        public List<MaxValue> PeakValues { get; set; }

        public Simulation(int index)
        {
            PeakValues = new List<MaxValue>();
            Index = index;
        }
        public void Insert(MaxValue value)
        {
            PeakValues.Add(value);
        }
    }
}

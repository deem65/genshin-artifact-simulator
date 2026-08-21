namespace ArtifactGeneratorLibrary
{
    /// <summary>
    /// a class made to hold infomation and or add infomation to list of results
    /// </summary>
    public class AverageValue
    {
        
        readonly int step;

        private List<MaxValue>? Stats { get; set; }
        public List<double> Values { get; private set; } = [];
        public List<int> Days { get; private set; } = [];
        public List<int> Totals { get; private set; } = [];

        public int Step => step;

        public AverageValue(int step)
        {
            this.step = step;
            Stats = [];
        }
        public void Finalize()
        {
            for (int i = 0; i < Stats.Count; i++)
            {
                Values.Add(Stats[i].Value);
                Days.Add(Stats[i].Day);
                Totals.Add(Stats[i].Total);
            }
        }

        public void Insert(MaxValue mv)
        {
            Stats.Add(mv);            
        }
        public override string ToString()
        {            
            return $"Value: {Step + 1}, Attempts: {Math.Round(Totals.Average() + 1, 2)}, Days: {Math.Round(Days.Average() + 1, 2)}";

        }
    }
}

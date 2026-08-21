namespace ArtifactGeneratorLibrary
{
    /// <summary>
    /// a struct made to hold infomation about a result of any kind
    /// </summary>
    public struct MaxValue(double value, int day, int total, int simulation = -1, int step = -1)
    {
        
        public double Value { get; set; } = value;
        public int Day { get; set; } = day;
        public int Total { get; set; } = total;
        public int Simulation { get; set; } = simulation;
        public int Step { get; set; } = step;
    }
}


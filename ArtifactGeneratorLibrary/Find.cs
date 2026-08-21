

namespace ArtifactGeneratorLibrary
{
    /// <summary>
    /// a class made to find artifacts and calculate average time
    /// </summary>   
    public class Find
    {
        private Random Random { get; } = new();
        private int TargetId { get; set; }
        public int Day { get; set; }
        public int Simulations { get; set; }
        public int Total { get; set; }
        private int Inventory { get; set; }
        private int Prev { get; set; }
        private int Level { get; set; }
        private double TargetValue { get; }
        private double MaxValue { get; set; } = 0;
        public int SimulationCount { get; private set; }
        public MaxValue[]? FinalMaxValues { get; private set; }
        public Simulation[]? SimulationsData { get; private set; }
        public Artifact? FinalArtifact { get; private set; }
        bool Drek { get; set; } = true;
        public Find(int targetId, double targetValue, int simulations, int level)
        {
            TargetId = targetId;
            Simulations = simulations;
            Level = level;
            TargetValue = targetValue;
            FinalMaxValues = new MaxValue[simulations];
            SimulationsData = new Simulation[simulations];

            for (int i = 0; i < simulations; i++)
            {
                SimulationsData[i] = new Simulation(simulations);
            }
        }
        public Find(int targetId, double targetValue, int level)
        {
            TargetId = targetId;
            Level = level;
            TargetValue = targetValue;
        }
        public void FindArtifact()
        {
            Inventory = 0;
            bool completed = false;
            while (!completed)
            {
                if (SearchNewForFind(true))
                {
                    break;
                }
                while (Inventory >= 3)
                {
                    Inventory -= 3;
                    if (SearchNewForFind(false))
                    {
                        completed = true;
                        break;
                    }
                }
            }
        }
        public void FindAverage()
        {            
            for (SimulationCount = 0; SimulationCount < Simulations; SimulationCount++)
            {
                Drek = true;
                Inventory = 0;
                MaxValue = 0;
                Day = 0;
                Total = 0;
                Prev = 0;
                bool completed = false;
                while (!completed)
                {
                    Day++;
                    int resin = 180;
                    if (Day % 7 == 0) //extra weekly resin from teapot 
                    {
                        resin += 60;
                    }
                    for (int i = 0; i < resin / 20; i++) //daily domain runs
                    {
                        completed = SearchNewForAverage(true);
                        if (Random.NextDouble() <= 0.07)
                        {
                            SearchNewForAverage(true);
                        }
                    }
                    while (Inventory >= 3) //strongbox (fodder 100% of artifacts)
                    {
                        Inventory -= 3;
                        completed = SearchNewForAverage(false);
                    }
                    if (Day % 15 == 0) //abyss
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            completed = SearchNewForAverage(false);
                        }
                    }
                }
            }
        }
        private bool SearchNewForFind(bool source)
        {
            var artifact = new Artifact(source);
            artifact.UpgradeToLevel(Level);
            Inventory++;
            Total++;
            if (artifact.GetStat(TargetId) >= TargetValue)
            {
                FinalArtifact = artifact;
                return true;
            }
            else return false;
        }

        private bool SearchNewForAverage(bool source) //true - from domain, false - from other source 
        {
            var artifact = new Artifact(source);
            artifact.UpgradeToLevel(Level);
            double t = artifact.GetStat(TargetId); ;
            if (t > MaxValue)
            {
                MaxValue = t;
                int limit = (int)MaxValue;
                if ((int)MaxValue > (int)TargetValue)
                {
                    limit = (int)TargetValue;
                }
                for (int index = Prev; index < limit; index++)
                {
                    SimulationsData[SimulationCount].Insert(new MaxValue(MaxValue, Day, Total, SimulationCount, index + 1));
                }
                Prev = limit;
            }
            Inventory++;
            Total++;
            if (MaxValue >= TargetValue && Drek)
            {
                //artifact.ShowcaseArtifact();
                FinalMaxValues[SimulationCount] = new MaxValue(MaxValue, Day, Total);
                Drek = false;
            }
            return MaxValue >= TargetValue;
        }

        
    }
}
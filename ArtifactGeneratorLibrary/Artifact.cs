namespace ArtifactGeneratorLibrary
{
    /// <summary>
    /// a model which holds all infomation about an artifact and can generate a random one.
    /// </summary>
    public class Artifact
    {
        public Random Random { get; } = new Random();
        public Piece Piece { get; set; }
        public Mainstat Mainstat { get; set; }
        public Substat[] Substats { get; set; } = new Substat[4];
        public int Level { get; set; }
        public bool IsFromDomain { get; set; }
        public int SubstatsAmount1 { get; set; }
        public double TotalWeight { get; set; }
        public double[] AllWeights { get; } = [6, 6, 6, 4, 4, 4, 4, 4, 3, 3];
        public double[] Percentages { get; private set; } = new double[10];

        public Artifact(bool isFromDomain)
        {
            IsFromDomain = isFromDomain;

            SubstatsAmount();

            Piece = new Piece();
            Mainstat = new Mainstat(Piece);

            if (Mainstat.Id != -1)
            {
                AllWeights[Mainstat.Id] = 0;
            }

            Update();
            for (int i = 0; i < SubstatsAmount1; i++)
            {
                GenerateSubstat(i);
            }
            if (SubstatsAmount1 == 3)
            {
                Substats[3] = new Substat();
            }
            Level = 0;
        }

        private void SubstatsAmount()
        {
            double x = Random.NextDouble();
            if (IsFromDomain)
            {
                if (x >= 0.2) SubstatsAmount1 = 3;
                else SubstatsAmount1 = 4;
            }
            else
            {
                if (x >= 0.34) SubstatsAmount1 = 3;
                else SubstatsAmount1 = 4;
            }
        }

        private void GenerateSubstat(int i)
        {
            double x = Random.NextDouble() * 100;

            for (int j = 0; j < Percentages.Length; j++)
            {
                if (x <= Percentages[j])
                {
                    Substats[i] = new Substat(j);
                    AllWeights[j] = 0;
                    break;
                }
            }
            Update();
        }
        public void Upgrade()
        {
            if (Substats[3].Empty)
            {
                double x = Random.NextDouble() * 100;
                for (int j = 0; j < Percentages.Length; j++)
                {
                    if (x <= Percentages[j])
                    {
                        Substats[3] = new Substat(j);
                        AllWeights[j] = 0;
                        break;
                    }
                }
            }
            else
            {
                int which = Random.Next(4);
                Substats[which].Upgrade();
            }
            Level += 4;

        }
        public void MaxUpgradeFromZero()
        {
            for (int i = 0; i < 5; i++)
            {
                Upgrade();
            }
        }
        public void UpgradeToLevel(int targetLevel)
        {
            if (targetLevel < Level)
            {
                throw new Exception();
            }
            while (Level < targetLevel - targetLevel % 4)
            {
                Upgrade();
            }
        }
        private void Update()
        {
            TotalWeight = ArraySum(AllWeights);

            double prev = 0;

            for (int i = 0; i < AllWeights.Length; i++)
            {
                Percentages[i] = AllWeights[i] / TotalWeight * 100 + prev;
                prev = Percentages[i];
            }
        }
        public static double ArraySum(double[] arr)
        {
            double sum = 0;
            for (int i = 0; i < arr.Length; i++)
                sum += arr[i];
            return sum;
        }
        public double GetStat(int id) //10 for cv / 11 for rv
        {
            double stat = 0;
            if (id == 10)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Substats[i].Name == "CritRate")
                    {
                        stat += Substats[i].Value * 2;
                    }
                    else if (Substats[i].Name == "CritDmg")
                    {
                        stat += Substats[i].Value;
                    }
                }
            }
            else if (id == 11)
            {
                for (int i = 0; i < 4; i++)
                {
                    stat += Substats[i].Rv;
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Substats[i].Id == id)
                    {
                        stat = Substats[i].Value;
                    }
                }
            }
            return stat;
        }
        public void ShowcaseArtifact()
        {
            Console.WriteLine("Piece: " + Piece.Name);
            Console.WriteLine("Mainstat: " + Mainstat.Name + "\n");
            Console.WriteLine("Level: " + Level + "\n");
            Console.WriteLine("Substats: ");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(Substats[i].ToString());
            }
            Console.WriteLine("\nCV: " + GetStat(10));
            Console.WriteLine("RV: " + GetStat(11) * 10);
            Console.WriteLine("\n----");
        }
    }
}

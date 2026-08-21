/// <summary>
/// a model which holds all infomation about the substat of an artifact, and can generate (random or not) / print one
/// </summary>

public class Substat
{
    /// <summary>

    /// IDs: 
    /// 0 - HP
    /// 1 - ATK
    /// 2 - DEF
    /// 3 - HP%
    /// 4 - ATK%
    /// 5 - DEF%
    /// 6 - EM
    /// 7 - ER
    /// 8 - CritRate
    /// 9 - CritDmg
    /// 
    /// </summary>
    public Random Random { get; } = new Random();
    public bool Empty { get; set; } = false;
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
    public int Rv { get; set; }
    public double[,] Values { get; } = {
            { 209.13, 239.00, 268.88, 298.75 },
            { 13.62, 15.56, 17.51, 19.45 },
            { 16.20, 18.52, 20.83, 23.15 },
            { 4.08, 4.66, 5.25, 5.83 },
            { 4.08, 4.66, 5.25, 5.83 },
            { 5.10, 5.83, 6.56, 7.29 },
            { 16.32, 18.65, 20.98, 23.31 },
            { 4.53, 5.18, 5.83, 6.48 },
            { 2.72, 3.11, 3.50, 3.89 },
            { 5.44, 6.22, 6.99, 7.77 } };

    public string[] StatNames { get; } = { "HP", "ATK", "DEF", "HP%", "ATK%", "DEF%", "EM", "ER", "CritRate", "CritDmg" };
    public Substat(int statId)
    {
        int x = Random.Next(4);
        Id = statId;
        Value = Values[statId, x];
        Name = StatNames[statId];
        Rv += 70 + 10 * x;
    }
    public Substat() => Empty = true;
    public override string ToString()
    {
        double v = Value;
        if (!Empty)
            return Name + " - " + Math.Round(v, 2) + " (" + Rv + "%)";
        else return string.Empty;
    }
    public void Upgrade()
    {
        int x = Random.Next(4);
        Value += Values[Id, x];
        Rv += 70 + 10 * x;
    }

}

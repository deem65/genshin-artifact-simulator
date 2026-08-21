/// <summary>
/// a model which holds all infomation about a mainstat of an artifact and can a random generate one based on givens 
/// </summary>
public class Mainstat
{
    
    public Random Random { get; } = new();

    public string Name { get; set; } = string.Empty;
    public int Id { get; set; }
    public Piece Piece { get; set; }

    public string[][] StatNames { get; } = [
            ["HP", ""],
            ["ATK", ""],
            ["HP%", "ATK%", "DEF%", "EM", "ER",""],
            ["HP%", "ATK%", "DEF%", "Pyro%", "Electro%", "Cryo%", "Hydro%", "Dendro%", "Anemo%", "Geo%", "Physical%", "EM", ""],
            ["HP%", "ATK%", "DEF%", "CritRate", "CritDmg", "HealingBonus", "EM", ""] ];

    public double[][] StatRanges { get; } = [
            [100],
            [100],
            [26.68, 53.34, 80, 90, 100],
            [19.25, 38.5, 57.5, 62.5, 67.5, 72.5, 77.5, 82.5, 87.5, 92.5, 97.5, 100],
            [22, 44, 66, 76, 86, 96, 100] ];

    public Mainstat(Piece piece)
    {
        this.Piece = piece;

        double x = Random.NextDouble() * 100;

        for (int i = 0; i < StatRanges[piece.Id].Length; i++)
        {
            if (x <= StatRanges[piece.Id][i])
            {
                Name = StatNames[piece.Id][i];
                break;
            }
        }
        UpdateMainstatId();
    }
    void UpdateMainstatId()
    {
        Id = Name switch
        {
            "HP" => 0,
            "ATK" => 1,
            "HP%" => 3,
            "ATK%" => 4,
            "DEF%" => 5,
            "EM" => 6,
            "ER" => 7,
            "CritRate" => 8,
            "CritDmg" => 9,
            _ => -1,
        };
    }
}

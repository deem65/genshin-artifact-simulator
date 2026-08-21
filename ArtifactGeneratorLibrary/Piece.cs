/// <summary>
/// a model which holds all infomation of an artifact main type, and can generate a random one
/// </summary>
public class Piece
{
    
    public Random Random { get; } = new();

    public int Id { get; set; }
    public string Name { get; set; }
    public string[] Pieces { get; set; } = ["Flower", "Feather", "Sands", "Goblet", "Circlet"];
    public Piece()
    {
        int x = Random.Next(Pieces.Length);
        Id = x;
        Name = Pieces[x];
    }
    public Piece(int pieceId)
    {
        Id = pieceId;
        Name = Pieces[pieceId];
    }

    
}

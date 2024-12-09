public class Card
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Meaning { get; set; }
    public bool IsRevealed { get; set; }
    public string Position { get; set; } // Past, Present, Future
}

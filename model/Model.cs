namespace Flashcards;
using Microsoft.EntityFrameworkCore;


public class FlashcardsContext : DbContext
{
    public DbSet<Deck> Decks { get; set; }
    public DbSet<Card> Cards { get; set; }

    public string DbPath { get; }

    public FlashcardsContext() {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "flashcards.db");
    }
}



public class Deck
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Card> Cards { get; } = new();
}

public class Card
{
    public int Id { get; set; }
    public string Front { get; set; }
    public string Back { get; set; }

    public int DeckId { get; set; }
    public Deck Deck { get; set; }

}
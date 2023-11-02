namespace Flashcards;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class FlashcardsContext : DbContext
{
    public DbSet<Deck> Decks => Set<Deck>();
    public DbSet<Card> Cards => Set<Card>();

    public string DbPath { get; }

    public FlashcardsContext(string? DbPath = null)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;

        // Create app folder in AppData/Local (MS Windows) if it doesn't exist yet
        var appPath = Path.Join(Environment.GetFolderPath(folder), "Flashcards_CS");
        Directory.CreateDirectory(appPath);

        this.DbPath = DbPath ?? Path.Join(appPath, "flashcards.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }
}



public class Deck
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required DateTime CreationTimestamp { get; set; }

    public List<Card> Cards { get; } = new();
}

public class Card
{
    public int Id { get; set; }
    public required string Front { get; set; }
    public required string Back { get; set; }
    public required DateTime CreationTimestamp { get; set; }
    public bool Tagged {get; set; } = false;

    public int DeckId { get; set; }
    public required Deck Deck { get; set; }

    public Card Clone() {
        return new Card() {Front = Front, Back = Back, Deck = Deck, CreationTimestamp = CreationTimestamp};
    }

    public static Card EmptyCard(Deck deck) {
        return new Card() {Front = "", Back = "", Deck = deck, CreationTimestamp = DateTime.UtcNow};
    }

}
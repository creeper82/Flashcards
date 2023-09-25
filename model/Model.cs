namespace Flashcards;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class FlashcardsContext : DbContext
{
    public DbSet<Deck> Decks => Set<Deck>();
    public DbSet<Card> Cards => Set<Card>();

    public string DbPath { get; }

    public FlashcardsContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "flashcards.db");
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

    public List<Card> Cards { get; } = new();
}

public class Card
{
    public int Id { get; set; }
    public required string Front { get; set; }
    public required string Back { get; set; }

    public int DeckId { get; set; }
    public required Deck Deck { get; set; }

}
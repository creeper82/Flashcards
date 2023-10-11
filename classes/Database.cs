using Microsoft.EntityFrameworkCore;

namespace Flashcards;

public class FlashcardsDatabase
{
    public FlashcardsContext db = new();
    public string Path = "";

    public FlashcardsDatabase()
    {
        Path = db.DbPath;
        db.Database.Migrate();
    }

    // READ

    public IEnumerable<Deck> GetDecks()
    {
        var decks = db.Decks
            .OrderBy(Deck => Deck.Name);
        return decks;
    }

    public IEnumerable<Card> GetAllCards()
    {
        var cards = db.Cards
            .OrderBy(Card => Card.Id);
        return cards;
    }

    public static IEnumerable<Card> GetDeckCards(Deck Deck)
    {
        var cards = Deck.Cards;
        return cards;
    }

    // CREATE

    public Deck CreateDeck(string Name)
    {
        var Deck = new Deck { Name = Name, CreationTimestamp = DateTime.UtcNow };
        db.Add(Deck);
        db.SaveChanges();

        return Deck;
    }

    public Card CreateCard(Deck Deck, string Front, string Back)
    {
        var Card = new Card { Front = Front, Back = Back, Deck = Deck, CreationTimestamp = DateTime.UtcNow };
        Deck.Cards.Add(Card);
        db.SaveChanges();

        return Card;
    }

    // UPDATE
    public void RenameDeck(Deck deck, string newName)
    {
        deck.Name = newName;
        db.SaveChanges();
    }

    public void UpdateCard(Card card, string? newFront = null, string? newBack = null) {
        if(newFront is not null) card.Front = newFront;
        if(newBack is not null) card.Back = newBack;
        db.SaveChanges();
    }

    // DELETE

    public void ResetAll()
    {
        db.Decks.ExecuteDelete();
    }

    public void RemoveDeck(Deck deck)
    {
        db.Remove(deck);
        db.SaveChanges();
    }

    public void RemoveCard(Card card) {
        db.Remove(card);
        db.SaveChanges();
    }
}
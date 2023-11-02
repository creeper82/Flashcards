using Microsoft.EntityFrameworkCore;

namespace Flashcards;

public class FlashcardsDatabase
{
    public FlashcardsContext context = new();
    public string Path = "";

    public FlashcardsDatabase()
    {
        Path = context.DbPath;
        context.Database.SetCommandTimeout(5);
        context.Database.Migrate();
    }

    // READ

    public IEnumerable<Deck> GetDecks()
    {
        var decks = context.Decks
            .OrderBy(Deck => Deck.Name)
            .Include(Deck => Deck.Cards);
        return decks;
    }

    public IEnumerable<Card> GetAllCards()
    {
        var cards = context.Cards
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
        context.Add(Deck);
        context.SaveChanges();

        return Deck;
    }

    public Card CreateCard(Deck Deck, string Front, string Back)
    {
        var Card = new Card { Front = Front, Back = Back, Deck = Deck, CreationTimestamp = DateTime.UtcNow };
        Deck.Cards.Add(Card);
        context.SaveChanges();

        return Card;
    }

    public void AppendCard(Card card)
    {
        context.Add(card);
        context.SaveChanges();
    }

    // UPDATE
    public void RenameDeck(Deck deck, string newName)
    {
        deck.Name = newName;
        context.SaveChanges();
    }

    public void UpdateCard(Card card, string? newFront = null, string? newBack = null, bool? newTaggedState = null)
    {
        if (newFront is not null) card.Front = newFront;
        if (newBack is not null) card.Back = newBack;
        if (newTaggedState is not null) card.Tagged = newTaggedState ?? false;
        context.SaveChanges();
    }

    // DELETE

    public void ResetAll()
    {
        context.Decks.ExecuteDelete();
    }

    public void RemoveDeck(Deck deck)
    {
        context.Remove(deck);
        context.SaveChanges();
    }

    public void RemoveCard(Card card)
    {
        context.Remove(card);
        context.SaveChanges();
    }
}
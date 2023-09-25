using Microsoft.EntityFrameworkCore;

namespace Flashcards;

public class Database
{
    public FlashcardsContext db = new();
    public string Path = "";

    public Database()
    {
        Path = db.DbPath;
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

    public IEnumerable<Card> GetDeckCards(Deck Deck)
    {
        var cards = Deck.Cards;
        return cards;
    }

    // CREATE

    public Deck CreateDeck(string Name)
    {
        var Deck = new Deck { Name = Name };
        db.Add(Deck);
        db.SaveChanges();

        return Deck;
    }

    public Card CreateCard(Deck Deck, string Front, string Back)
    {
        var Card = new Card { Front = Front, Back = Back, Deck = Deck };
        Deck.Cards.Add(Card);
        db.SaveChanges();

        return Card;
    }

    // REMOVE

    public void ResetAll()
    {
        db.Decks.ExecuteDelete();
    }
}
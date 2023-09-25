namespace Flashcards;

public class Database
{
    public FlashcardsContext db = new();
    public string Path = "";

    public Database()
    {
        Path = db.DbPath;
    }

    public IEnumerable<Deck> getDecks()
    {
        var decks = db.Decks
            .OrderBy(Deck => Deck.Name);
        return decks;
    }

    public IEnumerable<Card> getAllCards()
    {
        var cards = db.Cards
            .OrderBy(Card => Card.Id);
        return cards;
    }
}
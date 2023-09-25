namespace Flashcards;
using Microsoft.EntityFrameworkCore;

public class Deck
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Card> Cards { get; set; }
}

public class Card
{
    public int Id { get; set; }
    public string Front { get; set; }
    public string Back { get; set; }

    public int DeckId { get; set; }
    public Deck Deck {get; set;} 

}
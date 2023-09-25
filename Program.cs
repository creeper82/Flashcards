using Flashcards;

var db = new Database();

Console.WriteLine($"Database path is {db.Path}");

db.ResetAll();

var Deck = db.CreateDeck("Sample deck");
db.CreateCard(Deck, "Card_front", "Card_back");

var Card = db.GetDeckCards(Deck).First();
Console.WriteLine($"Card front is {Card.Front} and deck name is {Card.Deck.Name}");
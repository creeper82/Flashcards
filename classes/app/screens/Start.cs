namespace FlashcardsApp;

using Flashcards;
using CLI;

public static partial class App
{
    public static void Start(FlashcardsDatabase database)
    {
        database.ResetAll();
        // sample decks
        database.CreateDeck("Deck #01");
        database.CreateDeck("Deck #02");
        database.CreateDeck("Deck #03");
        database.CreateDeck("Deck #04");
        database.CreateDeck("Deck #05");
        database.CreateDeck("Deck #06");
        database.CreateDeck("Deck #07");
        database.CreateDeck("Deck #08");
        database.CreateDeck("Deck #09");
        database.CreateDeck("Deck #10");
        database.CreateDeck("Deck #11");
        database.CreateDeck("Deck #12");
        // sample cards
        database.CreateCard(database.GetDecks().First(), "feeble", "słaby");
        database.CreateCard(database.GetDecks().First(), "shrubs", "kitel");

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        ChoiceList<Deck> deckChoiceList = new(database.GetDecks())
        {
            PaginationCount = 5
        };

        bool running = true;

        while (running)
        {
            deckChoiceList.CheckOutOfBoundsPointer();
            Screens.Menu(deckChoiceList.PaginatedChoices, deckChoiceList.selectedIndex, deckChoiceList.PaginationStartIndex);
            
            running = Interactions.HandleMenu(database, deckChoiceList);
        }

    }
}
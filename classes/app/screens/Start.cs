namespace FlashcardsApp;

using Flashcards;
using CLI;

public static partial class App
{
    public static void Start(FlashcardsDatabase database)
    {
        database.ResetAll();
        database.CreateDeck("Aaaa");
        database.CreateDeck("Sample dec");
        database.CreateDeck("Another deck");
        database.CreateCard(database.GetDecks().First(), "a", "b");

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        ChoiceList<Deck> deckChoiceList = new(database.GetDecks());

        bool running = true;

        while (running)
        {
            deckChoiceList.CheckOutOfBoundsPointer();
            Screens.Menu(deckChoiceList.choices, deckChoiceList.selectedIndex);
            
            running = Interactions.HandleMenu(database, deckChoiceList);
        }

    }
}
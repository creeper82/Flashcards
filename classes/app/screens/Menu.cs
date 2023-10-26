namespace FlashcardsApp;

using Flashcards;
using CLI;

public static partial class App
{
    public static void Menu(FlashcardsDatabase database)
    {
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

            running = Logic.HandleMenu(database, deckChoiceList);
        }

    }
}
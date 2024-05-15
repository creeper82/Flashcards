namespace Flashcards;
using SharpViews;

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
            CLI.Screens.Menu(deckChoiceList.PaginatedChoices, deckChoiceList.SelectedIndex, deckChoiceList.PaginationStartIndex);

            running = Logic.HandleMenu(database, deckChoiceList);
        }

    }
}